namespace HomeBudgetManager.Services.Accounts.Domain.Accounts;

using HomeBudgetManager.Services.Accounts.Domain.Contractors;

public sealed class CreateAccountCommand(
    AccountId id,
    string name,
    int accountTypeId,
    string description,
    string currency,
    ContractorId? ownerId) : ICommand
{
    public AccountId Id { get; } = id;

    public string Name { get; } = name;

    public int AccountTypeId { get; } = accountTypeId;

    public string Description { get; } = description;

    public string Currency { get; } = currency;

    public ContractorId? OwnerId { get; } = ownerId;
}
