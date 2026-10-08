namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers;

using HomeBudgetManager.Services.Accounts.Application;
using HomeBudgetManager.Services.Accounts.Domain.Accounts;
using HomeBudgetManager.Services.Accounts.WebApi.Controllers.UserAccounts;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]

public class UserAccountsController(
    IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;

    [HttpGet]
    public IActionResult GetUserAccounts()
    {
        var result = new AccountInfo[]
        {
            new(Guid.NewGuid(), "Checking Account", "Checking", 1000.00m, 50.00m, "USD", true),
            new(Guid.NewGuid(), "Savings Account", "Savings", 5000.00m, 100.00m, "USD", true),
            new(Guid.NewGuid(), "Credit Card", "CreditCard", -200.00m, -20.00m, "USD", false),
        };

        return this.Ok(result);
    }

    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetUserAccountDetails(Guid accountId, CancellationToken cancellationToken)
    {
        var query = new GetAccountDetailsQuery(new AccountId(accountId));
        var result = await this.mediator.Fetch(query, cancellationToken);
        if (result is null)
        {
            return this.NotFound();
        }

        var output = new AccountDetails(
            result.Id.ToGuid(),
            result.Name,
            result.AccountType.Name,
            DateTimeOffset.UtcNow,
            result.Description,
            1000.00m,
            50.00m,
            result.Currency,
            true);

        return this.Ok(output);
    }

    [HttpPost]
    public IActionResult CreateUserAccount([FromBody] NewAccountData input)
    {
        var newAccountId = Guid.NewGuid();
        var account = new AccountDetails(
            newAccountId,
            input.Name,
            input.AccountType,
            DateTimeOffset.UtcNow,
            input.Description,
            0.00m,
            0.00m,
            input.Currency,
            true);
        return this.CreatedAtAction(nameof(this.GetUserAccountDetails), new { accountId = newAccountId }, account);
    }
}
