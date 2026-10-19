namespace HomeBudgetManager.Services.Accounts.Domain.Accounts.AggregateEvents;

using HomeBudgetManager.Services.Accounts.Domain.Contractors;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

public record class AccountCreated(
    AccountId Id,
    string Name,
    DateTimeOffset CreationDate,
    string Description,
    AccountType AccountType,
    string Currency,
    ContractorId? OwnerId) : IAggregateEvent, IAggregateCreationEvent
{
    public Guid AggregateId => this.Id.ToGuid();
}