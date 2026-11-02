namespace HomeBudgetManager.Services.Accounts.Sdk.UserAccounts;

using System.Collections.ObjectModel;

public sealed class GetUserAccountsResponse(
    IEnumerable<GetUserAccountsResponse.Account> accounts)
{
    public ReadOnlyCollection<Account> Accounts { get; } = new([.. (accounts ?? [])]);

    public sealed class Account(
        Guid id,
        string name,
        AccountType accountType,
        string currency,
        decimal balance,
        bool isActive)
    {
        public Guid Id { get; } = id;

        public string Name { get; } = name;

        public AccountType AccountType { get; } = accountType;

        public string Currency { get; } = currency;

        public decimal Balance { get; } = balance;

        public bool IsActive { get; } = isActive;
    }
}