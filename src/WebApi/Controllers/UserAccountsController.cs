namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers;

using HomeBudgetManager.Services.Accounts.Application;
using HomeBudgetManager.Services.Accounts.Application.Accounts.Projections.Handlers;
using HomeBudgetManager.Services.Accounts.Domain.Accounts;
using HomeBudgetManager.Services.Accounts.Domain.Accounts.AggregateEvents;
using HomeBudgetManager.Services.Accounts.WebApi.Controllers.UserAccounts;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]

public class UserAccountsController(
    IMediator mediator) : ControllerBase
{
    private const string GetAccountDetailsRouteName = "GetUserAccountDetails";

    private readonly IMediator mediator = mediator;

    [HttpGet]
    public async Task<IResult> GetUserAccounts(CancellationToken cancellationToken)
    {
        var query = new GetAccountsListQuery();
        var result = await this.mediator.Fetch(query, cancellationToken);
        var output = result.Accounts.Select(account => new AccountInfo(
            account.Id.ToGuid(),
            account.Name,
            account.AccountType.Name,
            account.Balance,
            account.CurrentPeriodChange,
            account.Currency,
            true)).ToArray();

        return Results.Ok(output);
    }

    [HttpGet("{accountId:guid}", Name = GetAccountDetailsRouteName)]
    public async Task<IResult> GetUserAccountDetails(Guid accountId, CancellationToken cancellationToken)
    {
        var query = new GetAccountDetailsQuery(new AccountId(accountId));
        var result = await this.mediator.Fetch(query, cancellationToken);
        if (result is null)
        {
            return Results.NotFound();
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

        return Results.Ok(output);
    }

    [HttpPost]
    public async Task<IResult> CreateUserAccount([FromBody] NewAccountData input, CancellationToken cancellationToken)
    {
        var command = new CreateAccountCommand(
            AccountId.New(),
            input.Name,
            input.AccountTypeId,
            input.Description,
            input.Currency,
            null);
        var result = await this.mediator.Execute(command, cancellationToken);
        return result.Match(
            _ => Results.CreatedAtRoute(GetAccountDetailsRouteName, new { accountId = command.Id.ToGuid() }),
            error => Results.InternalServerError(error.ErrorMessage));
    }
}
