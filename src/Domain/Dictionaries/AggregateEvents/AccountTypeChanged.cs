namespace HomeBudgetManager.Services.Accounts.Domain.Dictionaries.AggregateEvents;

public sealed record class AccountTypeChanged(
    Guid AggregateId,
    int Id,
    string Name) : IAggregateEvent;