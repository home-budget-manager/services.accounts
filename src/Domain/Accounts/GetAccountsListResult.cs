namespace HomeBudgetManager.Services.Accounts.Domain.Accounts;

using System.Collections.ObjectModel;

using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

public sealed class GetAccountsListResult(
    IEnumerable<GetAccountsListResult.AccountInfo> accounts) : IResult
{
    public ReadOnlyCollection<AccountInfo> Accounts { get; } = new ReadOnlyCollection<AccountInfo>([.. accounts]);

    public sealed class AccountInfo(
        AccountId id,
        string name,
        AccountType accountType,
        decimal balance,
        string currency,
        bool isActive)
    {
        public AccountId Id { get; } = id;

        public string Name { get; } = name;

        public AccountType AccountType { get; } = accountType;

        public decimal Balance { get; } = balance;

        public string Currency { get; } = currency;

        public bool IsActive { get; } = isActive;
    }
}
