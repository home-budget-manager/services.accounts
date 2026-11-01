namespace HomeBudgetManager.Services.Accounts.Application.Accounts;

using System.Security.Cryptography;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;

public sealed class GetAccountsListQueryHandler(
    IAccountInfoRepository accountInfoRepository) : QueryHandlerBase<GetAccountsListQuery, GetAccountsListResult>
{
    private readonly IAccountInfoRepository accountInfoRepository = accountInfoRepository;

    protected override async Task<GetAccountsListResult> Handle(GetAccountsListQuery query, CancellationToken cancellationToken)
    {
        var accounts = await this.accountInfoRepository.FindAccounts(cancellationToken);
        return new GetAccountsListResult([.. accounts.Select(a => new GetAccountsListResult.AccountInfo(
            a.Id,
            a.Name,
            a.AccountType,
            a.CurrentBalance,
            a.Currency,
            true))]);
    }
}
