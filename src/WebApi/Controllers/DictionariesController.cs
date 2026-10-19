namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers;

using System.Collections.ObjectModel;

using HomeBudgetManager.Services.Accounts.Application;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;
using HomeBudgetManager.Services.Accounts.WebApi.Controllers.Dictionaries;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class DictionariesController(
    IMediator mediator) : ControllerBase
{
    private const string GetAccountTypesRouteName = "GetAccountTypes";

    private readonly IMediator mediator = mediator;

    private static readonly Collection<Item> AccountTypes =
    [
        new(1, "Checking"),
        new(2, "Savings"),
        new(3, "Credit card"),
        new(4, "Cash wallet"),
    ];

    [HttpGet("account-types", Name = GetAccountTypesRouteName)]
    public async Task<IResult> GetAccountTypes(CancellationToken cancellationToken)
    {
        var result = await this.mediator.Fetch(new FetchAccountTypesQuery(), cancellationToken);
        var output = result.AccountTypes.Select(at => new Item(at.Id, at.Name)).ToList();
        return Results.Ok(output);
    }

    [HttpPost("account-types")]
    public async Task<IResult> CreateAccountType([FromBody] string displayName, CancellationToken cancellationToken)
    {
        var command = new CreateAccountTypeCommand(displayName);
        var result = await this.mediator.Execute(command, cancellationToken);
        if (result.IsT1)
        {
            return Results.InternalServerError();
        }

        var accountTypes = await this.mediator.Fetch(new FetchAccountTypesQuery(), cancellationToken);
        var created = accountTypes.AccountTypes.FirstOrDefault(at => at.Name == displayName);
        return Results.CreatedAtRoute(
            GetAccountTypesRouteName,
            routeValues: null,
            value: new { id = created.Id });
    }
}
