namespace HomeBudgetManager.Services.Accounts.Application;

using HomeBudgetManager.Services.Accounts.Application.Setup;
using HomeBudgetManager.Services.Accounts.Domain;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        Func<IConfigurationSetup, IConfigurationSetup.IDone> configure)
    {
        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<ICommandHandlerFactory, ServiceProviderCommandHandlerFactory>();
        services.AddScoped<IQueryHandlerFactory, ServiceProviderQueryHandlerFactory>();
        services
            .RegisterCommandHandler<Domain.Accounts.CreateAccountCommand, Accounts.CreateAccountCommandHandler>();

        services
            .RegisterQueryHandler<Domain.Accounts.GetAccountDetailsResult?, Accounts.GetAccountDetailsQueryHandler>()
            .RegisterQueryHandler<Domain.Accounts.GetAccountsListResult, Accounts.GetAccountsListQueryHandler>();

        services
            .RegisterProjectionHandler<Domain.Accounts.AggregateEvents.AccountCreated, Accounts.Projections.Handlers.AccountCreatedProjectionHandler>();
        var configurationSetup = new ConfigurationSetup();
        configure(configurationSetup);
        configurationSetup.RegisterServices(services);
        return services;
    }

    public static IServiceCollection RegisterCommandHandler<TCommand, TCommandHandler>(
        this IServiceCollection services)
        where TCommand : class, ICommand
        where TCommandHandler : class, ICommandHandler
    {
        var key = ServiceProviderCommandHandlerFactory.GetCommandHandlerKey<TCommand>();
        services.AddKeyedScoped<ICommandHandler, TCommandHandler>(key);
        return services;
    }

    public static IServiceCollection RegisterQueryHandler<TResult, THandler>(
        this IServiceCollection services)
        where TResult : IResult?
        where THandler : class, IQueryHandler<TResult>
    {
        var key = ServiceProviderQueryHandlerFactory.GetQueryHandlerKey<TResult>();
        services.AddKeyedScoped<IQueryHandler<TResult>, THandler>(key);
        return services;
    }

    public static IServiceCollection RegisterProjectionHandler<TEvent, THandler>(
        this IServiceCollection services)
        where TEvent : class, IAggregateEvent
        where THandler : class, IProjectionHandler
    {
        var key = typeof(TEvent).AssemblyQualifiedName!;
        services.AddKeyedTransient<IProjectionHandler, THandler>(key);
        return services;
    }
}
