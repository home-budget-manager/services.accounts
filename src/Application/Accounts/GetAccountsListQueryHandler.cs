namespace HomeBudgetManager.Services.Accounts.Application.Accounts;

using System.Security.Cryptography;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;

public sealed class GetAccountsListQueryHandler(
    IAccountInfoRepository accountInfoRepository) : QueryHandlerBase<GetAccountsListQuery, GetAccountsListResult>
{
    private static RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

    private readonly IAccountInfoRepository accountInfoRepository = accountInfoRepository;

    protected override async Task<GetAccountsListResult> Handle(GetAccountsListQuery query, CancellationToken cancellationToken)
    {
        var accounts = await this.accountInfoRepository.FindAccounts(cancellationToken);
        return new GetAccountsListResult(accounts.Select(a => new GetAccountsListResult.AccountInfo(
            a.Id,
            a.Name,
            a.AccountType,
            (decimal)GenerateRandomNumber() % 300000 / 100,
            (decimal)GenerateRandomNumber() % 15000 / 100,
            a.Currency,
            true)).ToList());
    }

    private static int GenerateRandomNumber()
    {
        var bytes = new byte[sizeof(int)]; // 4 bytes
        randomNumberGenerator.GetNonZeroBytes(bytes);
        var result = BitConverter.ToInt32(bytes);
        return result;
    }
}
