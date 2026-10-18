namespace HomeBudgetManager.Services.Accounts.Infrastructure.EventsSourcing.Kurrent;

using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using HomeBudgetManager.Services.Accounts.Domain;

using KurrentDB.Client;

using Microsoft.Extensions.Logging;

public sealed class ReadModelHydrator(
    KurrentDBPersistentSubscriptionsClient client,
    IProjectionHandlerFactory projectionHandlerFactory) : IReadModelHydrator
{
    public async Task Subscribe(CancellationToken cancellationToken)
    {
        await using var subscription = client.SubscribeToAll(
            "read-model-hydrator-subscription",
            cancellationToken: cancellationToken);
        try
        {
            await foreach (var message in subscription.Messages)
            {
                Task action = message switch
                {
                    PersistentSubscriptionMessage.SubscriptionConfirmation e => this.HandleSubscriptionConfirmation(subscription, e),
                    PersistentSubscriptionMessage.Event e => this.HandleEvent(subscription, e),
                    _ => this.HandleUnknownEvent(subscription, message),
                };

                await action;
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private async Task HandleSubscriptionConfirmation(
        KurrentDBPersistentSubscriptionsClient.PersistentSubscriptionResult _,
        PersistentSubscriptionMessage.SubscriptionConfirmation confirmation)
    {
        await Task.Yield();
    }

    private async Task HandleUnknownEvent(
        KurrentDBPersistentSubscriptionsClient.PersistentSubscriptionResult _,
        PersistentSubscriptionMessage message)
    {
        await Task.Yield();
    }

    private async Task HandleEvent(
        KurrentDBPersistentSubscriptionsClient.PersistentSubscriptionResult subscription,
        PersistentSubscriptionMessage.Event subscriptionEvent)
    {
        var resolvedEvent = subscriptionEvent.ResolvedEvent;
        try
        {
            //var rawMetadata = GetMetadata(resolvedEvent);
            //if (string.IsNullOrWhiteSpace(rawMetadata?.EventTypeName))
            //{
            //    await subscription.Ack([resolvedEvent]);
            //    return;
            //}

            //var metadata = rawMetadata.Value;
            var aggregateEvent = await GetEvent(resolvedEvent);
            if (aggregateEvent is null)
            {
                await subscription.Nack(PersistentSubscriptionNakEventAction.Unknown, "Failed to deserialize event", [resolvedEvent]);
                return;
            }

            await this.FeedProjections(resolvedEvent.OriginalStreamId, aggregateEvent, CancellationToken.None);

            await subscription.Ack([resolvedEvent]);
        }
        catch (Exception ex)
        {
            await subscription.Nack(
                PersistentSubscriptionNakEventAction.Park,
                "Error handling event",
                [resolvedEvent]);
        }
    }

    private async Task FeedProjections(string streamId, IAggregateEvent aggregateEvent, CancellationToken cancellationToken)
    {
        var workers = projectionHandlerFactory.Create(aggregateEvent)
            .ToList();
        var work = workers.Select(workers => workers.Handle(aggregateEvent, cancellationToken))
            .ToList();
        await Task.WhenAll(work);
    }

    private async Task<IAggregateEvent?> GetEvent(
        ResolvedEvent resolvedEvent)
    {
        var eventType = Type.GetType(resolvedEvent.Event.EventType);
        if (eventType is null)
        {
            return null;
        }

        var eventDataString = Encoding.UTF8.GetString(resolvedEvent.Event.Data.ToArray());
        return JsonSerializer.Deserialize(
            eventDataString,
            eventType,
            JsonSerializerOptionsFactory.GetOptions()) as IAggregateEvent;
    }

    private EventMetadata? GetMetadata(ResolvedEvent resolvedEvent)
    {
        var metadataString = Encoding.UTF8.GetString(resolvedEvent.Event.Metadata.ToArray());
        if (string.IsNullOrWhiteSpace(metadataString))
        {
            return null;
        }

        var metadata = JsonSerializer.Deserialize<EventMetadata>(
            metadataString,
            JsonSerializerOptionsFactory.GetOptions());
        return metadata;
    }
}
