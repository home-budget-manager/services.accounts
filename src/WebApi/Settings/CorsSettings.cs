namespace HomeBudgetManager.Services.Accounts.WebApi.Settings;

using Microsoft.AspNetCore.Cors.Infrastructure;

public sealed class CorsSettings
{
    public const string SectionName = "Cors";

    public string[] AllowedOrigins { get; init; } = ["*"];

    public string[] AllowedMethods { get; init; } = ["*"];

    public string[] AllowedHeaders { get; init; } = ["*"];

    public static CorsSettings LoadSettings(ConfigurationManager configuration)
    {
        return configuration.GetSection(SectionName).Get<CorsSettings>() ?? new();
    }

    public static void SetupCors(CorsOptions options, CorsSettings corsSettings)
    {
        options.AddDefaultPolicy(policyBuilder =>
        {
            Configure(policyBuilder, corsSettings.AllowedOrigins, static builder => builder.AllowAnyOrigin(), static (builder, values) => builder.WithOrigins(values));
            Configure(policyBuilder, corsSettings.AllowedMethods, static builder => builder.AllowAnyMethod(), static (builder, values) => builder.WithMethods(values));
            Configure(policyBuilder, corsSettings.AllowedHeaders, static builder => builder.AllowAnyHeader(), static (builder, values) => builder.WithHeaders(values));
        });
    }

    public static void Configure(
        CorsPolicyBuilder policyBuilder,
        string[] values,
        Action<CorsPolicyBuilder> allowAny,
        Action<CorsPolicyBuilder, string[]> allowConfigured)
    {
        if (values.Length == 0 || values.Contains("*", StringComparer.Ordinal))
        {
            allowAny(policyBuilder);
            return;
        }

        allowConfigured(policyBuilder, values);
    }
}
