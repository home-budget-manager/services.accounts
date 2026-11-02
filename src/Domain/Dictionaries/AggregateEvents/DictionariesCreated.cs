namespace HomeBudgetManager.Services.Accounts.Domain.Dictionaries.AggregateEvents;

public sealed record class DictionariesCreated(
    Guid AggregateId) : IAggregateEvent, IAggregateCreationEvent;
