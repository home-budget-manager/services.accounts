namespace HomeBudgetManager.Services.Accounts.Domain.Accounts.AggregateEvents;

using HomeBudgetManager.Services.Accounts.Domain.Contractors;

public record class AccountCreated(
    AccountId Id,
    string Name,
    DateTimeOffset CreationDate,
    string Description,
    AccountType AccountType,
    string Currency,
    ContractorId? OwnerId) : IAggregateEvent
{
    public Guid AggregateId => this.Id.ToGuid();
}