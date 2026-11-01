namespace HomeBudgetManager.Services.Accounts.Application;

using HomeBudgetManager.Services.Accounts.Domain;

using Microsoft.Extensions.DependencyInjection;

public sealed class ServiceProviderQueryHandlerFactory(
    IServiceProvider serviceProvider) : IQueryHandlerFactory
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public static string GetQueryHandlerKey<TResult>()
        where TResult : IResult?
    {
        return typeof(TResult).FullName ?? throw new InvalidOperationException("Unable to determine the full name of the result type.");
    }

    public IQueryHandler<TResult>? Create<TResult>()
        where TResult : IResult?
    {
        return serviceProvider.GetKeyedService<IQueryHandler<TResult>>(GetQueryHandlerKey<TResult>());
    }
}
