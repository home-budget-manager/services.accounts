namespace HomeBudgetManager.Services.Accounts.Sdk.UserAccounts.Payloads;

public sealed class AccountInfo(
    Guid accountId,
    string name,
    AccountType accountType,
    decimal balance,
    decimal currentPeriodChange,
    string currency,
    bool isActive)
{
    public Guid AccountId { get; } = accountId;

    public string Name { get; } = name;

    public AccountType AccountType { get; } = accountType;

    public decimal Balance { get; } = balance;

    public decimal CurrentPeriodChange { get; } = currentPeriodChange;

    public string Currency { get; } = currency;

    public bool IsActive { get; } = isActive;
}
