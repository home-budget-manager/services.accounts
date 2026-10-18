namespace HomeBudgetManager.Services.Accounts.Infrastructure.Projections.Mongo;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterMongoDbProjections(
        this IServiceCollection services,
        Action<MongoConfiguration> configure)
    {
        services.Configure<MongoConfiguration>(options =>
        {
            configure(options);
        });
        services.AddSingleton<IClientProvider, ClientProvider>();
        services.AddSingleton<IDatabaseProvider, DatabaseProvider>();
        services.AddSingleton<ICollectionProvider, CollectionProvider>();

        services.AddScoped<IAccountInfoRepository, Repositories.AccountInfoRepository>();

        return services;
    }
}
