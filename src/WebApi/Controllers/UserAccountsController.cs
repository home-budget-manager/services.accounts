namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers;

using HomeBudgetManager.Services.Accounts.WebApi.Controllers.UserAccounts;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]

public class UserAccountsController : ControllerBase
{
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
    public IActionResult GetUserAccountDetails(Guid accountId)
    {
        var result = new AccountDetails(
            accountId,
            "Checking Account",
            "Checking",
            DateTimeOffset.UtcNow,
            "This is a checking account.",
            1000.00m,
            50.00m,
            "USD",
            true);

        return this.Ok(result);
    }
}
