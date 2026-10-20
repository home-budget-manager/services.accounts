namespace HomeBudgetManager.Services.Accounts.Sdk;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAccountsSdk(
        this IServiceCollection services,
        ISettings settings)
    {
        services.AddHttpClient<UserAccounts.IUserAccountsClient, UserAccounts.UserAccountsClient>(settings);

        return services;
    }

    private static IServiceCollection AddHttpClient<TClient, TImplementation>(
        this IServiceCollection services,
        ISettings settings)
        where TClient : class
        where TImplementation : class, TClient
    {
        services.AddHttpClient<TClient, TImplementation>((sp, client) =>
        {
            client.BaseAddress = new Uri(settings.AccountsApiBaseUrl);
        });
        return services;
    }
}
