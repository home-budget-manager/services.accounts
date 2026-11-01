namespace HomeBudgetManager.Services.Accounts.Infrastructure.Projections.Mongo;

using HomeBudgetManager.Services.Accounts.Infrastructure.Projections.Mongo.Repositories;

using MongoDB.Driver;

public sealed class CollectionProvider(
    IDatabaseProvider databaseProvider) : ICollectionProvider
{
    private const string AccountsCollectionName = "Accounts";

    private readonly IDatabaseProvider databaseProvider = databaseProvider;

    public IMongoCollection<AccountInfoDto> Accounts => this.GetCollection<AccountInfoDto>(AccountsCollectionName);

    private IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        var database = this.databaseProvider.GetDatabase();
        return database.GetCollection<T>(collectionName);
    }
}
