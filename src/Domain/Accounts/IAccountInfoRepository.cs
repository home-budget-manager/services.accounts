namespace HomeBudgetManager.Services.Accounts.Domain.Accounts;

using HomeBudgetManager.Services.Accounts.Domain.Accounts.Projections;

public interface IAccountInfoRepository
{
    public Task InsertAccount(AccountInfo accountInfo, CancellationToken cancellationToken);
}
