namespace HomeBudgetManager.Services.Accounts.Sdk.UserAccounts;

public sealed class GetUserAccountRequest(
    Guid accountId)
{
    public Guid AccountId { get; } = accountId;
}
