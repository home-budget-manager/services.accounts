namespace HomeBudgetManager.Services.Accounts.WebApi.UnitTests.ControllersTests.UserAccountsTests;

using HomeBudgetManager.Services.Accounts.WebApi.Controllers.UserAccounts;

public class AccountInfoTests
{
    private Guid accountId;

    private string name = null!;

    private string accountType = null!;

    private decimal balance;

    private decimal currentPeriodChange;

    private string currency = null!;

    private bool isActive;

    private AccountInfo accountInfo = null!;

    [Fact]
    public void WhenAccountInfoIsCreatedThenItsPropertiesAreCorrect()
    {
        this.Given(t => t.AccountIdIs(Guid.NewGuid()))
            .And(t => t.NameIs("Checking Account"))
            .And(t => t.AccountTypeIs("Checking"))
            .And(t => t.BalanceIs(1000.00m))
            .And(t => t.CurrentPeriodChangeIs(50.00m))
            .And(t => t.CurrencyIs("USD"))
            .And(t => t.IsActiveIs(true))
            .When(t => t.AccountInfoIsCreated())
            .Then(t => t.AccountInfoPropertiesAreCorrect())
            .BDDfy();
    }

    private void AccountIdIs(Guid value) => this.accountId = value;

    private void NameIs(string value) => this.name = value;

    private void AccountTypeIs(string value) => this.accountType = value;

    private void BalanceIs(decimal value) => this.balance = value;

    private void CurrentPeriodChangeIs(decimal value) => this.currentPeriodChange = value;

    private void CurrencyIs(string value) => this.currency = value;

    private void IsActiveIs(bool value) => this.isActive = value;

    private void AccountInfoIsCreated()
    {
        this.accountInfo = new AccountInfo(
            this.accountId,
            this.name,
            this.accountType,
            this.balance,
            this.currentPeriodChange,
            this.currency,
            this.isActive);
    }

    private void AccountInfoPropertiesAreCorrect()
    {
        this.accountInfo.AccountId.ShouldBe(this.accountId);
        this.accountInfo.Name.ShouldBe(this.name);
        this.accountInfo.AccountType.ShouldBe(this.accountType);
        this.accountInfo.Balance.ShouldBe(this.balance);
        this.accountInfo.CurrentPeriodChange.ShouldBe(this.currentPeriodChange);
        this.accountInfo.Currency.ShouldBe(this.currency);
        this.accountInfo.IsActive.ShouldBe(this.isActive);
    }
}
