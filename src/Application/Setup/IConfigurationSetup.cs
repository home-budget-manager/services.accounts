namespace HomeBudgetManager.Services.Accounts.Application.Setup;

using HomeBudgetManager.Services.Accounts.Application.EventSourcing;

public interface IConfigurationSetup
{
    IDone WithEventsStorage<TEventsStorage>()
        where TEventsStorage : class, IEventsStorage;

    public interface IDone { }
}
