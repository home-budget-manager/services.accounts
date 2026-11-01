namespace HomeBudgetManager.Services.Accounts.WebApi.UnitTests.ControllersTests.UserAccountsControllerTests;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;
using HomeBudgetManager.Services.Accounts.Sdk.UserAccounts.Payloads;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

public class GetUserAccountDetailsTests : TestsBase
{
    private Guid accountId;

    private IResult result = null!;

    [Fact]
    public void WhenUserAccountDetailsAreRequestedThenCorrectAccountDetailsAreReturned()
    {
        this.Given(t => t.InstanceIsCreated())
            .And(t => t.AccountIdIs(Guid.NewGuid()))
            .And(t => t.AccountRetrievalIsMocked())
            .When(t => t.UserAccountDetailsAreRequested())
            .Then(t => t.ResultIsOkObjectResult())
            .And(t => t.ResultContentsAreCorrect())
            .BDDfy();
    }

    private void AccountIdIs(Guid value) => this.accountId = value;

    private void AccountRetrievalIsMocked()
    {
        var accountDetails = new GetAccountDetailsResult(
            new(this.accountId),
            "Checking Account",
            new(1, "TEST"),
            DateTimeOffset.UtcNow,
            "This is a checking account.",
            "USD",
            1000.00m,
            null,
            true);
        this.MediatorMock
            .Setup(m => m.Fetch(It.Is<GetAccountDetailsQuery>(q => q.Id.ToGuid() == this.accountId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(accountDetails);
    }

    private async Task UserAccountDetailsAreRequested()
    {
        this.result = await this.Instance.GetUserAccountDetails(this.accountId, CancellationToken.None);
    }

    private void ResultIsOkObjectResult()
    {
        result.ShouldBeOfType<Ok<AccountDetails>>();
    }

    private void ResultContentsAreCorrect()
    {
        var okResult = (Ok<AccountDetails>)this.result;
        okResult.Value.ShouldNotBeNull();
        var accountDetails = okResult.Value!;
        accountDetails.AccountId.ShouldBe(this.accountId);
        accountDetails.Name.ShouldBe("Checking Account");
        accountDetails.AccountType.ShouldBe("TEST");
        accountDetails.Description.ShouldBe("This is a checking account.");
        accountDetails.Balance.ShouldBe(1000.00m);
        accountDetails.Currency.ShouldBe("USD");
        accountDetails.IsActive.ShouldBe(true);
    }
}
