namespace HomeBudgetManager.Services.Accounts.Domain.Accounts;

using HomeBudgetManager.Services.Accounts.Domain.Contractors;

public sealed class GetAccountDetailsResult(
    AccountId id,
    string name,
    AccountType accountType,
    string description,
    string currency,
    ContractorId? ownerId) : IResult
{
    public AccountId Id { get; } = id;

    public string Name { get; } = name;

    public AccountType AccountType { get; } = accountType;

    public string Description { get; } = description;

    public string Currency { get; } = currency;

    public ContractorId? OwnerId { get; } = ownerId;
}
