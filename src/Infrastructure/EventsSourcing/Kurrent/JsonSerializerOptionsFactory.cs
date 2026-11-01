namespace HomeBudgetManager.Services.Accounts.Infrastructure.EventsSourcing.Kurrent;

using System.Text.Json;
using System.Text.Json.Serialization;

public static class JsonSerializerOptionsFactory
{
    private static readonly Lazy<JsonSerializerOptions> options = new(CreateOptions, true);

    public static JsonSerializerOptions GetOptions() => options.Value;

    private static JsonSerializerOptions CreateOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            Converters =
            {
                new JsonStringEnumConverter(),
            },
        };
    }
}
