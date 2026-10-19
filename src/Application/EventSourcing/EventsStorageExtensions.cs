namespace HomeBudgetManager.Services.Accounts.Application.EventSourcing;

using HomeBudgetManager.Services.Accounts.Domain;

public static class EventsStorageExtensions
{
    public static async Task Store(
        this IEventsStorage eventsStorage,
        IAggregateRoot aggregateRoot,
        CancellationToken cancellationToken)
    {
        var events = aggregateRoot.GetUncommittedEvents();
        var aggregateName = aggregateRoot.AggregateName;
        var aggregateId = aggregateRoot.AggregateId;
        await eventsStorage.Store(aggregateName, aggregateId, events, cancellationToken);
    }

    public static async Task<TAggregate?> Load<TAggregate>(
        this IEventsStorage eventsStorage,
        string aggregateName,
        Guid aggregateId,
        Func<IEnumerable<IAggregateEvent>, TAggregate> createAggregateFromEvents,
        CancellationToken cancellationToken)
        where TAggregate : IAggregateRoot
    {
        var events = await eventsStorage.Retrieve(aggregateName, aggregateId, cancellationToken);
        if (events.Count == 0)
        {
            return default;
        }

        var aggregate = createAggregateFromEvents(events);
        return aggregate;
    }
}
