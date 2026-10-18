namespace HomeBudgetManager.Services.Accounts.Infrastructure.Projections.Mongo;

using MongoDB.Driver;

public interface IClientProvider
{
    IMongoClient GetClient();
}
