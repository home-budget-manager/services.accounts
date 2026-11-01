namespace HomeBudgetManager.Services.Accounts.WebApi.Settings;

using HomeBudgetManager.Services.Accounts.Infrastructure.EventsSourcing.Kurrent;

using KurrentDB.Client;

public class KurrentDbSettings
{
    public const string SectionName = "KurrentDb";

    public string ConnectionString { get; init; } = string.Empty;

    public ReadModelHydratorSettings ReadModelHydration { get; init; } = new()
    {
        SubscriptionName = string.Empty,
        CheckpointAfterMs = 0,
        CheckpointLowerBound = 0,
        CheckpointUpperBound = 0
    };

    public static KurrentDbSettings LoadSettings(ConfigurationManager configuration)
    {
        return configuration.GetSection(SectionName).Get<KurrentDbSettings>() ?? new();
    }

    public static void RegisterConnection(IServiceCollection services, KurrentDbSettings settings)
    {
        services.AddSingleton(sp => CreateClient(settings));
        services.AddSingleton(sp => CreatePersistentSubscriptionsClient(settings));
        services.RegisterKurrentDbReadModelHydration();
    }

    private static KurrentDBClient CreateClient(KurrentDbSettings settings)
    {
        var clientSettings = KurrentDBClientSettings.Create(settings.ConnectionString);
        var client = new KurrentDBClient(clientSettings);
        return client;
    }

    private static KurrentDBPersistentSubscriptionsClient CreatePersistentSubscriptionsClient(KurrentDbSettings settings)
    {
        var clientSettings = KurrentDBClientSettings.Create(settings.ConnectionString);
        var client = new KurrentDBPersistentSubscriptionsClient(clientSettings);
        return client;
    }

    public sealed class ReadModelHydratorSettings
    {
        public required string SubscriptionName { get; init; } = default!;

        public required int CheckpointAfterMs { get; init; }

        public required int CheckpointLowerBound { get; init; }

        public required int CheckpointUpperBound { get; init; }
    }
}
