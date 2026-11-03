namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers;

using System.Collections.ObjectModel;

using HomeBudgetManager.Services.Accounts.Application;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;
using HomeBudgetManager.Services.Accounts.Sdk.Dictionaries.Payloads;

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public sealed class DictionariesController(
    IMediator mediator) : ControllerBase
{
    private const string GetAccountTypesRouteName = "GetAccountTypes";

    private readonly IMediator mediator = mediator;

    [HttpGet("account-types", Name = GetAccountTypesRouteName)]
    public async Task<IResult> GetAccountTypes(CancellationToken cancellationToken)
    {
        var result = await this.mediator.Fetch(new FetchAccountTypesQuery(), cancellationToken);
        var output = result.AccountTypes.Select(at => new DictionaryItem(at.Id, at.Name)).ToList();
        return Results.Ok(output);
    }

    [HttpPut("account-types/{id}")]
    public async Task<IResult> UpdateAccountType(
        [FromRoute] int id,
        [FromBody] string displayName,
        CancellationToken cancellationToken)
    {
        var accountTypes = await this.mediator.Fetch(new FetchAccountTypesQuery(), cancellationToken);
        if (!accountTypes.AccountTypes.Any(at => at.Id == id))
        {
            return Results.NotFound();
        }

        if (string.IsNullOrWhiteSpace(displayName))
        {
            return Results.BadRequest();
        }

        var command = new UpdateAccountTypeCommand(id, displayName);
        var result = await this.mediator.Execute(command, cancellationToken);
        return result.Match(
            _ => Results.NoContent(),
            _ => Results.InternalServerError());
    }

    [HttpPost("account-types")]
    public async Task<IResult> CreateAccountType([FromBody] string displayName, CancellationToken cancellationToken)
    {
        var command = new CreateAccountTypeCommand(displayName);
        var result = await this.mediator.Execute(command, cancellationToken);
        return result.Match(
            _ => Results.CreatedAtRoute(
                GetAccountTypesRouteName),
            _ => Results.InternalServerError());
    }
}
