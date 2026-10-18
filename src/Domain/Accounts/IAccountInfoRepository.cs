namespace HomeBudgetManager.Services.Accounts.Domain.Accounts;

using HomeBudgetManager.Services.Accounts.Domain.Accounts.Projections;

public interface IAccountInfoRepository
{
    Task InsertAccount(AccountInfo accountInfo, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<AccountInfo>> FindAccounts(CancellationToken cancellationToken);

    Task<AccountInfo?> GetAccount(Guid accountId, CancellationToken cancelationToken);
}
