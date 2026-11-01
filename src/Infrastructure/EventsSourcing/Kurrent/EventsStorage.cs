namespace HomeBudgetManager.Services.Accounts.Infrastructure.EventsSourcing.Kurrent;

using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

using HomeBudgetManager.Services.Accounts.Application.EventSourcing;
using HomeBudgetManager.Services.Accounts.Domain;

using KurrentDB.Client;

public sealed class EventsStorage(
    KurrentDBClient client) : IEventsStorage
{
    private static readonly JsonSerializerOptions serializerOptions = JsonSerializerOptionsFactory.GetOptions();

    private readonly KurrentDBClient client = client;

    public async Task<IReadOnlyCollection<IAggregateEvent>> Retrieve(string aggregateName, Guid aggregateId, CancellationToken cancellationToken)
    {
        var streamName = $"{aggregateName}_{aggregateId}";

        var eventsPromise = client.ReadStreamAsync(
            Direction.Forwards,
            streamName,
            StreamPosition.Start,
            cancellationToken: cancellationToken);
        var readState = await eventsPromise.ReadState;
        if (readState == ReadState.StreamNotFound)
        {
            return [];
        }

        var events = await eventsPromise.ToListAsync(cancellationToken: cancellationToken);
        if (events.Count == 0)
        {
            return [];
        }

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
            var deserialized = System.Text.Json.JsonSerializer.Deserialize(eventString, eventType, serializerOptions);
            if (deserialized is null)
            {
                continue;
            }

            var deserializedEvent = (IAggregateEvent)deserialized;
            result[i] = deserializedEvent;
        }

        return result;
    }

    public async Task Store(string aggregateName, Guid aggregateId, IEnumerable<IAggregateEvent> events, long expectedVersion, CancellationToken cancellationToken)
    {
        var mapped = events
            .Select(e => ConvertToKurrentEvents(e, aggregateName, aggregateId));
        foreach (var (streamName, isNewStream, eventData) in mapped)
        {
            if (expectedVersion == 0)
            {
                if (!isNewStream)
                {
                    throw new InvalidOperationException($"Stream '{streamName}' was not loaded before appending.");
                }

                await client.AppendToStreamAsync(
                    streamName,
                    StreamState.NoStream,
                    [eventData],
                    cancellationToken: cancellationToken);
            }
            else
            {
                await client.AppendToStreamAsync(
                    streamName,
                    StreamState.StreamRevision((ulong)(expectedVersion - 1)),
                    [eventData],
                    cancellationToken: cancellationToken);
            }

            expectedVersion++;
        }
    }

    private static (string StreamName, bool IsNewStream, EventData EventData) ConvertToKurrentEvents(
        IAggregateEvent @event,
        string aggregateName,
        Guid aggregateId)
    {
        var streamName = $"{aggregateName}_{aggregateId}";
        var eventType = @event.GetType().AssemblyQualifiedName;
        if (eventType == null)
        {
            throw new InvalidOperationException($"Event type '{@event.GetType()}' not found.");
        }

        var eventDataBytes = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), serializerOptions);
        var eventData = new EventData(
            Uuid.NewUuid(),
            eventType,
            eventDataBytes);
        return (streamName, @event is IAggregateCreationEvent, eventData);
    }
}
