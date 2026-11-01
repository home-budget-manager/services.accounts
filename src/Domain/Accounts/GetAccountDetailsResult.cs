namespace HomeBudgetManager.Services.Accounts.Domain.Accounts;

using HomeBudgetManager.Services.Accounts.Domain.Contractors;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

public sealed class GetAccountDetailsResult(
    AccountId id,
    string name,
    AccountType accountType,
    DateTimeOffset creationDate,
    string description,
    string currency,
    decimal currentBalance,
    ContractorId? ownerId,
    bool isActive) : IResult
{
    public AccountId Id { get; } = id;

    public string Name { get; } = name;

    public AccountType AccountType { get; } = accountType;

    public DateTimeOffset CreationDate { get; } = creationDate;

    public string Description { get; } = description;

    public string Currency { get; } = currency;

    public decimal CurrentBalance { get; } = currentBalance;

    public ContractorId? OwnerId { get; } = ownerId;

    public bool IsActive { get; } = isActive;
}
