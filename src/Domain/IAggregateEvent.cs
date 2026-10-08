namespace HomeBudgetManager.Services.Accounts.Domain;

public interface IAggregateEvent
{
    Guid AggregateId { get; }
}
