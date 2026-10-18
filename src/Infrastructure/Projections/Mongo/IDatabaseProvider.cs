namespace HomeBudgetManager.Services.Accounts.Infrastructure.Projections.Mongo;

using MongoDB.Driver;

public interface IDatabaseProvider
{
    IMongoDatabase GetDatabase();
}
