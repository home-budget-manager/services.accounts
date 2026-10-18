namespace HomeBudgetManager.Services.Accounts.Infrastructure.Projections.Mongo;

using HomeBudgetManager.Services.Accounts.Infrastructure.Projections.Mongo.Repositories;

using MongoDB.Driver;

public interface ICollectionProvider
{
    IMongoCollection<AccountInfoDto> Accounts { get; }
}
