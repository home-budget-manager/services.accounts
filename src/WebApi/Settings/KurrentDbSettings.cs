namespace HomeBudgetManager.Services.Accounts.WebApi.Settings;

using KurrentDB.Client;

public class KurrentDbSettings
{
    public const string SectionName = "KurrentDb";

    public string ConnectionString { get; init; } = string.Empty;

    public static KurrentDbSettings LoadSettings(ConfigurationManager configuration)
    {
        return configuration.GetSection(SectionName).Get<KurrentDbSettings>() ?? new();
    }

    public static void RegisterConnection(IServiceCollection services, KurrentDbSettings settings)
    {
        services.AddSingleton(sp => CreateClient(settings));
    }

    private static KurrentDBClient CreateClient(KurrentDbSettings settings)
    {
        var clientSettings = KurrentDBClientSettings.Create(settings.ConnectionString);
        var client = new KurrentDBClient(clientSettings);
        return client;
    }
}
