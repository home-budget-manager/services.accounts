namespace HomeBudgetManager.Services.Accounts.Sdk.UserAccounts.Payloads;

public sealed class NewAccountData(
    string name,
    int accountTypeId,
    string description,
    string currency)
{
    public string Name { get; } = name;

    public int AccountTypeId { get; } = accountTypeId;

    public string Description { get; } = description;

    public string Currency { get; } = currency;
}
