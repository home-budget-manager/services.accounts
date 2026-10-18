namespace HomeBudgetManager.Services.Accounts.Application.Accounts;

using System.Security.Cryptography;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;

public sealed class GetAccountDetailsQueryHandler(
    IAccountInfoRepository accountInfoRepository) : QueryHandlerBase<GetAccountDetailsQuery, GetAccountDetailsResult?>
{
    private static RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

    private readonly IAccountInfoRepository accountInfoRepository = accountInfoRepository;

    protected override async Task<GetAccountDetailsResult?> Handle(
        GetAccountDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var account = await this.accountInfoRepository.GetAccount(query.Id.ToGuid(), cancellationToken);

        if (account is null)
        {
            return null;
        }

        return new GetAccountDetailsResult(
            account.Id,
            account.Name,
            account.AccountType,
            account.Description,
            account.Currency,
            account.OwnerId);
    }
}
