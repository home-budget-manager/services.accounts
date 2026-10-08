namespace HomeBudgetManager.Services.Accounts.Application.Accounts.ReadModel;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;
using HomeBudgetManager.Services.Accounts.Domain.Contractors;

public sealed record AccountInfo(
    AccountId Id,
    string Name,
    AccountType AccountType,
    string Description,
    string Currency,
    ContractorId? OwnerId);
