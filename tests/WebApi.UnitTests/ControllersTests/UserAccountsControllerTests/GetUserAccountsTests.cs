namespace HomeBudgetManager.Services.Accounts.WebApi.UnitTests.ControllersTests.UserAccountsControllerTests;

using HomeBudgetManager.Services.Accounts.WebApi.Controllers.UserAccounts;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public class GetUserAccountsTests : TestsBase
{
    private IResult result = null!;

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
        result.ShouldBeOfType<Ok<AccountInfo[]>>();
    }

    private void ResultContentsAreCorrect()
    {
        var okResult = (Ok<AccountInfo[]>)this.result;
        okResult.Value.ShouldNotBeNull();
        var accounts = okResult.Value!;
        accounts.Length.ShouldBe(3);
        accounts[0].Name.ShouldBe("Checking Account");
        accounts[1].Name.ShouldBe("Savings Account");
        accounts[2].Name.ShouldBe("Credit Card");
    }
}
