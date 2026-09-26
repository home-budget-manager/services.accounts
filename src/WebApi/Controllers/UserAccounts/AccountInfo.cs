namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers.UserAccounts;

public class AccountInfo
{
    public AccountInfo(
        Guid accountId,
        string name,
        string accountType,
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

    public string AccountType { get; }

    public decimal Balance { get; }

    public decimal CurrentPeriodChange { get; }

    public string Currency { get; }

    public bool IsActive { get; }
}
