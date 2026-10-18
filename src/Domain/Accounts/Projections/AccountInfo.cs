namespace HomeBudgetManager.Services.Accounts.Domain.Accounts.Projections;

using HomeBudgetManager.Services.Accounts.Domain.Contractors;

public sealed record AccountInfo(
    AccountId Id,
    string Name,
    DateTimeOffset CreationDate,
    string Description,
    AccountType AccountType,
    string Currency,
    ContractorId? OwnerId);
