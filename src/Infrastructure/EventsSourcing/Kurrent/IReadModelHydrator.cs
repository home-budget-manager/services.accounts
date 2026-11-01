namespace HomeBudgetManager.Services.Accounts.Infrastructure.EventsSourcing.Kurrent;

public interface IReadModelHydrator
{
    Task Subscribe(string subscriptionName, CancellationToken cancellationToken);
}
