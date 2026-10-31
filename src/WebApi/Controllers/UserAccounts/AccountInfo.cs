namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers.UserAccounts;

using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

public class AccountInfo
{
    public AccountInfo(
        Guid accountId,
        string name,
        AccountType accountType,
        decimal balance,
        decimal currentPeriodChange,
        string currency,
        bool isActive)
    {
        this.AccountId = accountId;
        this.Name = name;
        this.AccountType = accountType;
        this.Balance = balance;
        this.CurrentPeriodChange = currentPeriodChange;
        this.Currency = currency;
        this.IsActive = isActive;
    }

    public Guid AccountId { get; }

    public string Name { get; }

    public AccountType AccountType { get; }

    public decimal Balance { get; }

    public decimal CurrentPeriodChange { get; }

    public string Currency { get; }

    public bool IsActive { get; }
}
