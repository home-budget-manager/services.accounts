namespace HomeBudgetManager.Services.Accounts.Infrastructure.EventsSourcing.Kurrent;

public interface IReadModelHydrator
{
    Task Subscribe(CancellationToken cancellationToken);
}
