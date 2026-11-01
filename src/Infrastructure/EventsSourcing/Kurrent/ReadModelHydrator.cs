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
                    PersistentSubscriptionMessage.SubscriptionConfirmation e => HandleSubscriptionConfirmation(subscription, e),
                    PersistentSubscriptionMessage.Event e => this.HandleEvent(subscription, e),
                    _ => HandleUnknownEvent(subscription, message),
                };

                await action;
            }
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async static Task HandleSubscriptionConfirmation(
        KurrentDBPersistentSubscriptionsClient.PersistentSubscriptionResult _,
        PersistentSubscriptionMessage.SubscriptionConfirmation __)
    {
        await Task.Yield();
    }

    private async static Task HandleUnknownEvent(
        KurrentDBPersistentSubscriptionsClient.PersistentSubscriptionResult _,
        PersistentSubscriptionMessage __)
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
            var aggregateEvent = await GetEvent(resolvedEvent);
            if (aggregateEvent is null)
            {
                await subscription.Nack(PersistentSubscriptionNakEventAction.Unknown, "Failed to deserialize event", [resolvedEvent]);
                return;
            }

            await this.FeedProjections(resolvedEvent.OriginalStreamId, aggregateEvent, CancellationToken.None);

            await subscription.Ack([resolvedEvent]);
        }
        catch (Exception)
        {
            await subscription.Nack(
                PersistentSubscriptionNakEventAction.Retry,
                "Error handling event",
                [resolvedEvent]);
        }
    }

    private async Task FeedProjections(string _, IAggregateEvent aggregateEvent, CancellationToken cancellationToken)
    {
        var workers = projectionHandlerFactory.Create(aggregateEvent)
            .ToList();
        var work = workers.Select(workers => workers.Handle(aggregateEvent, cancellationToken))
            .ToList();
        await Task.WhenAll(work);
    }

    private async static Task<IAggregateEvent?> GetEvent(
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
}
