namespace HomeBudgetManager.Services.Accounts.Infrastructure.Projections.Mongo;

using Microsoft.Extensions.DependencyInjection;

using MongoDB.Driver;

public sealed class ClientProvider : IClientProvider
{
    private readonly IServiceProvider serviceProvider;

    public ClientProvider(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public IMongoClient GetClient()
    {
        return this.serviceProvider.GetRequiredService<IMongoClient>();
    }
}
