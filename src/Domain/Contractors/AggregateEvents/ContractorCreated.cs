namespace HomeBudgetManager.Services.Accounts.Domain.Contractors.AggregateEvents;

public record class ContractorCreated(
    ContractorId Id,
    string Name) : IAggregateEvent
{
    public Guid AggregateId => this.Id.ToGuid();
}