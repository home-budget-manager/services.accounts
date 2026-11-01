namespace HomeBudgetManager.Services.Accounts.Application;

using HomeBudgetManager.Services.Accounts.Domain;

public abstract class ProjectionHandlerBase<TEvent> : IProjectionHandler
    where TEvent : IAggregateEvent
{
    public Task Handle(IAggregateEvent @event, CancellationToken cancellationToken)
    {
        if (@event is not TEvent typedEvent)
        {
            throw new InvalidOperationException($"Invalid event type. Expected '{typeof(TEvent).FullName}', but received '{@event.GetType().FullName}'.");
        }

        return this.Handle(typedEvent, cancellationToken);
    }

    protected abstract Task Handle(TEvent @event, CancellationToken cancellationToken);
}
