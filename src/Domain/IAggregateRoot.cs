namespace HomeBudgetManager.Services.Accounts.Domain;

public interface IAggregateRoot
{
    Guid AggregateId { get; }

    string AggregateName { get; }

    IEnumerable<IAggregateEvent> GetUncommittedEvents();
}
