namespace HomeBudgetManager.Services.Accounts.WebApi.Settings;

using HomeBudgetManager.Services.Accounts.Infrastructure.Projections.Mongo;

using MongoDB.Driver;

public class MongoDbSettings
{
    public const string SectionName = "MongoDb";

    public string ConnectionString { get; init; } = null!;

    public string DatabaseName { get; init; } = null!;

    public static MongoDbSettings LoadSettings(ConfigurationManager configuration)
    {
        return configuration.GetSection(SectionName).Get<MongoDbSettings>() ?? new();
    }

    public static void RegisterConnection(IServiceCollection services, MongoDbSettings settings)
    {
        services.AddSingleton<IMongoClient>(sp => CreateClient(settings));
    }

    public static void RegisterMongoDbProjections(IServiceCollection services, MongoDbSettings settings)
    {
        services.RegisterMongoDbProjections(options =>
        {
            options.DatabaseName = settings.DatabaseName;
        });
    }

    private static MongoClient CreateClient(MongoDbSettings settings)
    {
        var clientSettings = MongoClientSettings.FromConnectionString(settings.ConnectionString);
        var client = new MongoClient(clientSettings);
        return client;
    }
}
