namespace HomeBudgetManager.Services.Accounts.Sdk.UserAccounts.Payloads;

public sealed class AccountType(
    int id,
    string name)
{
    public int Id { get; } = id;

    public string Name { get; } = name;
}
