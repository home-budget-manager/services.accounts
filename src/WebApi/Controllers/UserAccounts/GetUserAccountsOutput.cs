namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers.UserAccounts;

public sealed class GetUserAccountsOutput(
    AccountInfo[] accounts)
{
    public AccountInfo[] Accounts { get; } = accounts;
}
