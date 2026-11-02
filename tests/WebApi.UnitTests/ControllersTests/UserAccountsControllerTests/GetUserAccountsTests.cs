namespace HomeBudgetManager.Services.Accounts.WebApi.UnitTests.ControllersTests.UserAccountsControllerTests;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;
using HomeBudgetManager.Services.Accounts.Sdk.UserAccounts.Payloads;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

public sealed class GetUserAccountsTests : TestsBase
{
    private IResult result = null!;

    [Fact]
    public void WhenUserAccountsAreRequestedThenCorrectAccountsAreReturned()
    {
        this.Given(t => t.InstanceIsCreated())
            .And(t => t.UserAccountIsMocked())
            .When(t => t.UserAccountsAreRequested())
            .Then(t => t.ResultIsOkObjectResult())
            .And(t => t.ResultContentsAreCorrect())
            .BDDfy();
    }

    private void UserAccountIsMocked()
    {
        this.MediatorMock.Setup(m => m.Fetch(It.IsAny<GetAccountsListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetAccountsListResult(
            [
                new GetAccountsListResult.AccountInfo(
                    new AccountId(Guid.NewGuid()),
                    "Checking Account",
                    new(1, "Checking"),
                    1000.00m,
                    "USD",
                    true),
                new GetAccountsListResult.AccountInfo(
                    new AccountId(Guid.NewGuid()),
                    "Savings Account",
                    new(2, "Savings"),
                    5000.00m,
                    "USD",
                    true),
                new GetAccountsListResult.AccountInfo(
                    new AccountId(Guid.NewGuid()),
                    "Credit Card",
                    new(3, "Credit Card"),
                    -2000.00m,
                    "USD",
                    true)
            ]));
    }

    private async Task UserAccountsAreRequested()
    {
        this.result = await this.Instance.GetUserAccounts(CancellationToken.None);
    }

    private void ResultIsOkObjectResult()
    {
        result.ShouldBeOfType<Ok<GetUserAccountsOutput>>();
    }

    private void ResultContentsAreCorrect()
    {
        var okResult = (Ok<GetUserAccountsOutput>)this.result;
        okResult.Value.ShouldNotBeNull();
        var accounts = okResult.Value!.Accounts;
        accounts.Length.ShouldBe(3);
        accounts[0].Name.ShouldBe("Checking Account");
        accounts[1].Name.ShouldBe("Savings Account");
        accounts[2].Name.ShouldBe("Credit Card");
    }
}
