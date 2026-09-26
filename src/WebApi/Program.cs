using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var corsSettings = builder.Configuration.GetSection(CorsSettings.SectionName).Get<CorsSettings>() ?? new();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        Configure(policyBuilder, corsSettings.AllowedOrigins, static builder => builder.AllowAnyOrigin(), static (builder, values) => builder.WithOrigins(values));
        Configure(policyBuilder, corsSettings.AllowedMethods, static builder => builder.AllowAnyMethod(), static (builder, values) => builder.WithMethods(values));
        Configure(policyBuilder, corsSettings.AllowedHeaders, static builder => builder.AllowAnyHeader(), static (builder, values) => builder.WithHeaders(values));
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void Configure(
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

sealed class CorsSettings
{
    public const string SectionName = "Cors";

    public string[] AllowedOrigins { get; init; } = ["*"];

    public string[] AllowedMethods { get; init; } = ["*"];

    public string[] AllowedHeaders { get; init; } = ["*"];
}
