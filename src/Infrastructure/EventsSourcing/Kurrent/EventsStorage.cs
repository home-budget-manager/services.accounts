namespace HomeBudgetManager.Services.Accounts.Infrastructure.EventsSourcing.Kurrent;

using System.Collections.ObjectModel;

using HomeBudgetManager.Services.Accounts.Application.EventSourcing;
using HomeBudgetManager.Services.Accounts.Domain;

using KurrentDB.Client;

public sealed class EventsStorage(
    KurrentDBClient client) : IEventsStorage
{
    private readonly KurrentDBClient client = client;

    public async Task<IReadOnlyCollection<IAggregateEvent>> Retrieve(Guid aggregateId, CancellationToken cancellationToken)
    {
        var streamName = aggregateId.ToString();
        var eventsPromise = client.ReadStreamAsync(
            Direction.Forwards,
            streamName,
            StreamPosition.Start,
            cancellationToken: cancellationToken);
        var events = await eventsPromise.ToListAsync(cancellationToken: cancellationToken);
        var result = new IAggregateEvent[events.Count];
        for (var i = 0; i < events.Count; i++)
        {
            var @event = events[i];
            var eventType = Type.GetType(@event.OriginalEvent.EventType);
            if (eventType == null)
            {
                throw new InvalidOperationException($"Event type '{@event.OriginalEvent.EventType}' not found.");
            }

            var eventData = @event.OriginalEvent.Data;
            var eventString = System.Text.Encoding.UTF8.GetString(eventData.ToArray());
            var deserialized = System.Text.Json.JsonSerializer.Deserialize(eventString, eventType);
            if (deserialized is null)
            {
                continue;
            }

            var deserializedEvent = (IAggregateEvent)deserialized;
            result[i] = deserializedEvent;
        }

        return result;
    }

    public async Task Store(IEnumerable<IAggregateEvent> events, CancellationToken cancellationToken)
    {
        var mapped = events
            .Select(ConvertToKurrentEvents);
        foreach (var (streamName, isNewStream, eventData) in mapped)
        {
            var expectedState = isNewStream ? StreamState.NoStream : StreamState.StreamExists;
            await client.AppendToStreamAsync(
                streamName,
                expectedState,
                [eventData],
                cancellationToken: cancellationToken);
        }
    }

    private static (string StreamName, bool IsNewStream, EventData EventData) ConvertToKurrentEvents(IAggregateEvent @event)
    {
        var streamName = @event.AggregateId.ToString();
        var eventType = @event.GetType().AssemblyQualifiedName;
        if (eventType == null)
        {
            throw new InvalidOperationException($"Event type '{@event.GetType()}' not found.");
        }

        var eventDataBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType());
        var eventData = new EventData(
            Uuid.NewUuid(),
            eventType,
            eventDataBytes);
        return (streamName, @event is IAggregateCreationEvent, eventData);
    }
}
