namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers.UserAccounts;

public class NewAccountData(
    string name,
    string accountType,
    string description,
    string currency)
{
    public string Name { get; } = name;

    public string AccountType { get; } = accountType;

    public string Description { get; } = description;

    public string Currency { get; } = currency;
}
