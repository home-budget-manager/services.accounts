namespace HomeBudgetManager.Services.Accounts.Sdk.UserAccounts.Payloads;

public sealed class GetUserAccountsOutput(
    AccountInfo[] accounts)
{
    public AccountInfo[] Accounts { get; } = accounts;
}