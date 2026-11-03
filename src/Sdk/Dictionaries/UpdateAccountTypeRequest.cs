namespace HomeBudgetManager.Services.Accounts.Sdk.Dictionaries;

public sealed class UpdateAccountTypeRequest(
    AccountType accountType)
{
    public AccountType AccountData { get; } = accountType;
}
