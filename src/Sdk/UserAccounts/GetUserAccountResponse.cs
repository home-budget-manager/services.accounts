namespace HomeBudgetManager.Services.Accounts.Sdk.UserAccounts;

using System;

public sealed class GetUserAccountResponse(
    GetUserAccountResponse.AccountDetails details)
{
    public AccountDetails Details { get; } = details;

    public sealed class AccountDetails(
        Guid id,
        string name,
        AccountType accountType,
        string description,
        DateTimeOffset creationDate,
        string currency,
        decimal balance,
        bool isActive)
    {
        public Guid Id { get; } = id;

        public string Name { get; } = name;

        public AccountType AccountType { get; } = accountType;

        public string Description { get; } = description;

        public DateTimeOffset CreationDate { get; } = creationDate;

        public string Currency { get; } = currency;

        public decimal Balance { get; } = balance;

        public bool IsActive { get; } = isActive;
    }
}
