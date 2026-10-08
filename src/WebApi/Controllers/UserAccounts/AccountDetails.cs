namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers.UserAccounts;

public class AccountDetails(
    Guid accountId,
    string name,
    string accountType,
    DateTimeOffset creationDate,
    string description,
    decimal balance,
    decimal currentPeriodChange,
    string currency,
    bool isActive)
{
    public Guid AccountId { get; } = accountId;

    public string Name { get; } = name;

    public string AccountType { get; } = accountType;

    public DateTimeOffset CreationDate { get; } = creationDate;

    public string Description { get; } = description;

    public decimal Balance { get; } = balance;

    public decimal CurrentPeriodChange { get; } = currentPeriodChange;

    public string Currency { get; } = currency;

    public bool IsActive { get; } = isActive;
}
