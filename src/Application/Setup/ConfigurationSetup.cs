namespace HomeBudgetManager.Services.Accounts.Application.Setup;

using HomeBudgetManager.Services.Accounts.Application.EventSourcing;
using Microsoft.Extensions.DependencyInjection;

internal sealed class ConfigurationSetup : IConfigurationSetup
{
    private Type? eventsStorageImplementation;

    IConfigurationSetup.IDone IConfigurationSetup.WithEventsStorage<TEventsStorage>()
    {
        this.eventsStorageImplementation = typeof(TEventsStorage);
        return new Done();
    }

    public void RegisterServices(IServiceCollection services)
    {
        if (this.eventsStorageImplementation is null)
        {
            throw new InvalidOperationException("Events storage implementation is not configured.");
        }

        services.AddScoped(typeof(IEventsStorage), this.eventsStorageImplementation);
    }

    private sealed class Done: IConfigurationSetup.IDone
    {
    }
}
