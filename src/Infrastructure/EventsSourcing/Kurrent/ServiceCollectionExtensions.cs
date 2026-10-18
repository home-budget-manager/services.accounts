namespace HomeBudgetManager.Services.Accounts.Infrastructure.EventsSourcing.Kurrent;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterKurrentDbReadModelHydration(
        this IServiceCollection services)
    {
        services.AddScoped<IReadModelHydrator, ReadModelHydrator>();
        services.AddScoped<IProjectionHandlerFactory, ProjectionHandlerFactory>();

        return services;
    }
}
