namespace HomeBudgetManager.Services.Accounts.Sdk.UserAccounts;

public sealed class CreateAccountResponse
{
    public CreateAccountResponse(Guid accountId)
    {
        this.AccountId = accountId;
    }

    public Guid AccountId { get; }
}
