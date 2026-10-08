namespace HomeBudgetManager.Services.Accounts.Domain.Contractors;

public sealed class ContractorAggregate : AggregateRootBase
{
    private ContractorAggregate()
    {
    }

    public override Guid AggregateId => this.Id.ToGuid();

    public ContractorId Id { get; private set; } = default;

    public string Name { get; private set; } = string.Empty;

    public static ContractorAggregate Create(ContractorId id, string name)
    {
        var contractor = new ContractorAggregate();
        var @event = new AggregateEvents.ContractorCreated(id, name);
        contractor.ApplyEvent(@event);
        return contractor;
    }

    public static ContractorAggregate CreateFromEvents(IEnumerable<IAggregateEvent> events)
    {
        var contractor = new ContractorAggregate();
        contractor.ApplyEventsWithoutRecording(events);
        return contractor;
    }

    protected override void When(IAggregateEvent @event)
    {
        switch (@event)
        {
            case AggregateEvents.ContractorCreated e:
                When(e);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void When(AggregateEvents.ContractorCreated @event)
    {
        this.Id = @event.Id;
        this.Name = @event.Name;
    }
}
