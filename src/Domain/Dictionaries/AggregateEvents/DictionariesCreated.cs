namespace HomeBudgetManager.Services.Accounts.Domain.Dictionaries.AggregateEvents;

public record class DictionariesCreated(
    Guid AggregateId) : IAggregateEvent, IAggregateCreationEvent;
