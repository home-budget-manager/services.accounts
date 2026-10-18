namespace HomeBudgetManager.Services.Accounts.Infrastructure.Projections.Mongo;

using Microsoft.Extensions.Options;

using MongoDB.Driver;

public sealed class DatabaseProvider(
    IClientProvider clientProvider,
    IOptions<MongoConfiguration> options) : IDatabaseProvider
{
    private readonly IClientProvider clientProvider = clientProvider;

    private readonly IOptions<MongoConfiguration> options = options;

    public IMongoDatabase GetDatabase()
    {
        var databaseName = this.options.Value.DatabaseName;
        var client = this.clientProvider.GetClient();
        return client.GetDatabase(databaseName);
    }
}
