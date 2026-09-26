namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers.UserAccounts;

public class AccountDetails
{
    public AccountDetails(
        Guid accountId,
        string name,
        string accountType,
        DateTimeOffset creationDate,
        string description,
        decimal balance,
        decimal currentPeriodChange,
        string currency,
        bool isActive)
    {
        this.AccountId = accountId;
        this.Name = name;
        this.AccountType = accountType;
        this.CreationDate = creationDate;
        this.Description = description;
        this.Balance = balance;
        this.CurrentPeriodChange = currentPeriodChange;
        this.Currency = currency;
        this.IsActive = isActive;
    }

    public Guid AccountId { get; }

    public string Name { get; }

    public string AccountType { get; }

    public DateTimeOffset CreationDate { get; }

    public string Description { get; }

    public decimal Balance { get; }

    public decimal CurrentPeriodChange { get; }

    public string Currency { get; }

    public bool IsActive { get; }
}
