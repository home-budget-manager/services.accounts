namespace HomeBudgetManager.Services.Accounts.WebApi.UnitTests.ControllersTests.UserAccountsControllerTests;

using HomeBudgetManager.Services.Accounts.WebApi.Controllers.UserAccounts;

using Microsoft.AspNetCore.Mvc;

public class GetUserAccountsTests : TestsBase
{
    private IActionResult result = null!;

    [Fact]
    public void WhenUserAccountsAreRequestedThenCorrectAccountsAreReturned()
    {
        this.Given(t => t.InstanceIsCreated())
            .When(t => t.UserAccountsAreRequested())
            .Then(t => t.ResultIsOkObjectResult())
            .And(t => t.ResultContentsAreCorrect())
            .BDDfy();
    }

    private void UserAccountsAreRequested()
    {
        this.result = this.Instance.GetUserAccounts();
    }

    private void ResultIsOkObjectResult()
    {
        result.ShouldBeOfType<OkObjectResult>();
    }

    private void ResultContentsAreCorrect()
    {
        var okResult = (OkObjectResult)this.result;
        okResult.Value.ShouldNotBeNull();
        okResult.Value!.ShouldBeOfType<AccountInfo[]>();
        var accounts = (AccountInfo[])okResult.Value!;
        accounts.Length.ShouldBe(3);
        accounts[0].Name.ShouldBe("Checking Account");
        accounts[1].Name.ShouldBe("Savings Account");
        accounts[2].Name.ShouldBe("Credit Card");
    }
}
