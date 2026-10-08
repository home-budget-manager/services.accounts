namespace HomeBudgetManager.Services.Accounts.Domain.Accounts;

using HomeBudgetManager.Services.Accounts.Domain.Contractors;

public sealed class CreateAccountCommand(
    AccountId Id,
    string Name,
    AccountType AccountType,
    string Description,
    string Currency,
    ContractorId? OwnerId) : ICommand
{
    public AccountId Id { get; } = Id;

    public string Name { get; } = Name;

    public AccountType AccountType { get; } = AccountType;

    public string Description { get; } = Description;

    public string Currency { get; } = Currency;

    public ContractorId? OwnerId { get; } = OwnerId;
}
