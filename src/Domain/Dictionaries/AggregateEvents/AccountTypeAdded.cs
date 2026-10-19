namespace HomeBudgetManager.Services.Accounts.Domain.Dictionaries.AggregateEvents;

public record class AccountTypeAdded(
    Guid AggregateId,
    int Id,
    string Name) : IAggregateEvent;