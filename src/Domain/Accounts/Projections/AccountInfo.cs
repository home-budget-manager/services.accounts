namespace HomeBudgetManager.Services.Accounts.Domain.Accounts.Projections;

using HomeBudgetManager.Services.Accounts.Domain.Contractors;

public sealed record AccountInfo(
    AccountId Id,
    string Name,
    DateTimeOffset CreationDate,
    DateTimeOffset LastModificationDate,
    string Description,
    AccountType AccountType,
    decimal CurrentBalance,
    string Currency,
    ContractorId? OwnerId,
    bool IsActive);
