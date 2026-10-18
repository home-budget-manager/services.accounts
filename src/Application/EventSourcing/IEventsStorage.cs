namespace HomeBudgetManager.Services.Accounts.Application.EventSourcing;

using HomeBudgetManager.Services.Accounts.Domain;

public interface IEventsStorage
{
    Task Store(IEnumerable<IAggregateEvent> events, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<IAggregateEvent>> Retrieve(Guid aggregateId, CancellationToken cancellationToken);
}
