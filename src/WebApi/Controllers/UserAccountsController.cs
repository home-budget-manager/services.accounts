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
    public IResult GetUserAccounts()
    {
        var result = new AccountInfo[]
        {
            new(Guid.NewGuid(), "Checking Account", "Checking", 1000.00m, 50.00m, "USD", true),
            new(Guid.NewGuid(), "Savings Account", "Savings", 5000.00m, 100.00m, "USD", true),
            new(Guid.NewGuid(), "Credit Card", "CreditCard", -200.00m, -20.00m, "USD", false),
        };

        return Results.Ok(result);
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
