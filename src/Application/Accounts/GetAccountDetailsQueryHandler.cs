namespace HomeBudgetManager.Services.Accounts.Application.Accounts;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;

public sealed class GetAccountDetailsQueryHandler : QueryHandlerBase<GetAccountDetailsQuery, GetAccountDetailsResult?>
{
    protected override async Task<GetAccountDetailsResult?> Handle(
        GetAccountDetailsQuery query,
        CancellationToken cancellationToken)
    {
        await Task.Yield();
        var account = Application.ReadModel.GetAccountById(query.Id);
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
