namespace HomeBudgetManager.Services.Accounts.Application;

using HomeBudgetManager.Services.Accounts.Domain;

public interface IProjectionHandler
{
    Task Handle(IAggregateEvent @event, CancellationToken cancellationToken);
}
