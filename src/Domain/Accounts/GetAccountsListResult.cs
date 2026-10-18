namespace HomeBudgetManager.Services.Accounts.Domain.Accounts;

using System.Collections.ObjectModel;

public sealed class GetAccountsListResult : IResult
{
    public GetAccountsListResult(IEnumerable<AccountInfo> accounts)
    {
        this.Accounts = new ReadOnlyCollection<AccountInfo>([.. accounts]);
    }

    public ReadOnlyCollection<AccountInfo> Accounts { get; }

    public sealed class AccountInfo
    {
        public AccountInfo(
            AccountId id,
            string name,
            AccountType accountType,
            decimal balance,
            decimal currentPeriodChange,
            string currency,
            bool isActive)
        {
            this.Id = id;
            this.Name = name;
            this.AccountType = accountType;
            this.Balance = balance;
            this.CurrentPeriodChange = currentPeriodChange;
            this.Currency = currency;
            this.IsActive = isActive;
        }
        public AccountId Id { get; }

        public string Name { get; }

        public AccountType AccountType { get; }

        public decimal Balance { get; }

        public decimal CurrentPeriodChange { get; }

        public string Currency { get; }

        public bool IsActive { get; }
    }
}
