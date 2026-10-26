namespace HomeBudgetManager.Services.Accounts.WebApi.UnitTests.ControllersTests.UserAccountsControllerTests;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;
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
            .And(t => t.UserAccountIsMocked())
            .When(t => t.UserAccountsAreRequested())
            .Then(t => t.ResultIsOkObjectResult())
            .And(t => t.ResultContentsAreCorrect())
            .BDDfy();
    }

    private void UserAccountIsMocked()
    {
        this.MediatorMock.Setup(m => m.Fetch(It.IsAny<GetAccountsListQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new GetAccountsListResult(new[]
            {
                new GetAccountsListResult.AccountInfo(
                    new AccountId(Guid.NewGuid()),
                    "Checking Account",
                    new AccountType(1, "Checking"),
                    1000.00m,
                    50.00m,
                    "USD",
                    true),
                new GetAccountsListResult.AccountInfo(
                    new AccountId(Guid.NewGuid()),
                    "Savings Account",
                    new AccountType(2, "Savings"),
                    5000.00m,
                    100.00m,
                    "USD",
                    true),
                new GetAccountsListResult.AccountInfo(
                    new AccountId(Guid.NewGuid()),
                    "Credit Card",
                    new AccountType(3, "Credit Card"),
                    -2000.00m,
                    -50.00m,
                    "USD",
                    true)
            }));
    }

    private async Task UserAccountsAreRequested()
    {
        this.result = await this.Instance.GetUserAccounts(CancellationToken.None);
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
