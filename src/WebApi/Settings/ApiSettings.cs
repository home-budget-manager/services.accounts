namespace HomeBudgetManager.Services.Accounts.WebApi.Settings;

public class ApiSettings
{
    public const string SectionName = "Api";

    public bool ScalarEnabled { get; init; } = true;

    public static ApiSettings LoadSettings(ConfigurationManager configuration)
    {
        return configuration.GetSection(SectionName).Get<ApiSettings>() ?? new();
    }
}
