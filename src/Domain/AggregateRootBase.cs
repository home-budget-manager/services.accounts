namespace HomeBudgetManager.Services.Accounts.Domain;

public abstract class AggregateRootBase : IAggregateRoot
{
    private readonly List<IAggregateEvent> _uncommittedEvents = [];

    public abstract Guid AggregateId { get; }

    public virtual string AggregateName => this.GetType().Name;

    public IEnumerable<IAggregateEvent> GetUncommittedEvents() => this._uncommittedEvents.AsReadOnly();

    protected void ApplyEvent(IAggregateEvent @event)
    {
        this.When(@event);
        this._uncommittedEvents.Add(@event);
    }

    protected void ApplyEventWithoutRecording(IAggregateEvent @event)
    {
        this.When(@event);
    }

    protected void ApplyEventsWithoutRecording(IEnumerable<IAggregateEvent> events)
    {
        foreach (var @event in events)
        {
            this.ApplyEventWithoutRecording(@event);
        }
    }

    protected abstract void When(IAggregateEvent @event);
}
