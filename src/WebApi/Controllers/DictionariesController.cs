namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers;

using System.Collections.ObjectModel;

using HomeBudgetManager.Services.Accounts.WebApi.Controllers.Dictionaries;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class DictionariesController : ControllerBase
{
    private static readonly Collection<Item> AccountTypes =
    [
        new(1, "Checking"),
        new(2, "Savings"),
        new(3, "Credit card"),
        new(4, "Cash wallet"),
    ];

    [HttpGet("account-types")]
    public IActionResult GetAccountTypes()
    {
        var result = AccountTypes.ToArray();
        return this.Ok(result);
    }

    [HttpPost("account-types")]
    public IActionResult CreateAccountType([FromBody] string displayName)
    {
        var newItem = new Item(AccountTypes.Count + 1, displayName);
        AccountTypes.Add(newItem);
        return this.CreatedAtAction(nameof(this.GetAccountTypes), new { id = newItem.Id }, newItem);
    }
}
