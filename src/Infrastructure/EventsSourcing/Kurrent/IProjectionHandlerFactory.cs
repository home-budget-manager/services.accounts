namespace HomeBudgetManager.Services.Accounts.Infrastructure.EventsSourcing.Kurrent;

using HomeBudgetManager.Services.Accounts.Application;
using HomeBudgetManager.Services.Accounts.Domain;

public interface IProjectionHandlerFactory
{
    IEnumerable<IProjectionHandler> Create(IAggregateEvent @event);
}
