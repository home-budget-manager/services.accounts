namespace HomeBudgetManager.Services.Accounts.Sdk.UserAccounts.Payloads;

public sealed class AccountDetails(
    Guid accountId,
    string name,
    string accountType,
    DateTimeOffset creationDate,
    string description,
    decimal balance,
    string currency,
    bool isActive)
{
    public Guid AccountId { get; } = accountId;

    public string Name { get; } = name;

    public string AccountType { get; } = accountType;

    public DateTimeOffset CreationDate { get; } = creationDate;

    public string Description { get; } = description;

    public decimal Balance { get; } = balance;

    public string Currency { get; } = currency;

    public bool IsActive { get; } = isActive;
}
