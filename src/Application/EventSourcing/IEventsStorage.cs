namespace HomeBudgetManager.Services.Accounts.Application.EventSourcing;

using HomeBudgetManager.Services.Accounts.Domain;

public interface IEventsStorage
{
    Task Store(string aggregateName, Guid aggregateId, IEnumerable<IAggregateEvent> events, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<IAggregateEvent>> Retrieve(string aggregateName, Guid aggregateId, CancellationToken cancellationToken);
}
