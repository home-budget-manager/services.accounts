namespace HomeBudgetManager.Services.Accounts.Sdk;

using System.Text.Json;

public static class SerializationOptionsProvider
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static JsonSerializerOptions GetJsonSerializerOptions() => JsonSerializerOptions;

    public static void SetJsonSerializerOptions(JsonSerializerOptions output)
    {
        output.PropertyNamingPolicy = JsonSerializerOptions.PropertyNamingPolicy;
    }
}
