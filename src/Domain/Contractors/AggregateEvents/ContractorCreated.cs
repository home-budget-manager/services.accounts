namespace HomeBudgetManager.Services.Accounts.Domain.Contractors.AggregateEvents;

public sealed record class ContractorCreated(
    ContractorId Id,
    string Name) : IAggregateEvent, IAggregateCreationEvent
{
    public Guid AggregateId => this.Id.ToGuid();
}