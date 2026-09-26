namespace HomeBudgetManager.Services.Accounts.WebApi.UnitTests.ControllersTests.UserAccountsControllerTests;

using HomeBudgetManager.Services.Accounts.WebApi.Controllers.UserAccounts;

using Microsoft.AspNetCore.Mvc;

public class GetUserAccountDetailsTests : TestsBase
{
    private Guid accountId;

    private IActionResult result = null!;

    [Fact]
    public void WhenUserAccountDetailsAreRequestedThenCorrectAccountDetailsAreReturned()
    {
        this.Given(t => t.InstanceIsCreated())
            .And(t => t.AccountIdIs(Guid.NewGuid()))
            .When(t => t.UserAccountDetailsAreRequested())
            .Then(t => t.ResultIsOkObjectResult())
            .And(t => t.ResultContentsAreCorrect())
            .BDDfy();
    }

    private void AccountIdIs(Guid value) => this.accountId = value;

    private void UserAccountDetailsAreRequested()
    {
        this.result = this.Instance.GetUserAccountDetails(this.accountId);
    }

    private void ResultIsOkObjectResult()
    {
        result.ShouldBeOfType<OkObjectResult>();
    }

    private void ResultContentsAreCorrect()
    {
        var okResult = (OkObjectResult)this.result;
        okResult.Value.ShouldNotBeNull();
        okResult.Value!.ShouldBeOfType<AccountDetails>();
        var accountDetails = (AccountDetails)okResult.Value!;
        accountDetails.AccountId.ShouldBe(this.accountId);
        accountDetails.Name.ShouldBe("Checking Account");
        accountDetails.AccountType.ShouldBe("Checking");
        accountDetails.Description.ShouldBe("This is a checking account.");
        accountDetails.Balance.ShouldBe(1000.00m);
        accountDetails.CurrentPeriodChange.ShouldBe(50.00m);
        accountDetails.Currency.ShouldBe("USD");
        accountDetails.IsActive.ShouldBe(true);
    }
}
