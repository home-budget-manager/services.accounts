namespace HomeBudgetManager.Services.Accounts.Infrastructure.EventsSourcing.Kurrent;

using HomeBudgetManager.Services.Accounts.Application;
using HomeBudgetManager.Services.Accounts.Domain;

using Microsoft.Extensions.DependencyInjection;

public sealed class ProjectionHandlerFactory(
    IServiceProvider serviceProvider) : IProjectionHandlerFactory
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public IEnumerable<IProjectionHandler> Create(IAggregateEvent @event)
    {
        var eventType = @event.GetType();
        var handlers = serviceProvider.GetKeyedServices<IProjectionHandler>(eventType.AssemblyQualifiedName!);
        return handlers;
    }
}
