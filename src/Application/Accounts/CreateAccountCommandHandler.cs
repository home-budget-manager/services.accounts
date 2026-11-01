namespace HomeBudgetManager.Services.Accounts.Application.Accounts;

using HomeBudgetManager.Services.Accounts.Application.EventSourcing;
using HomeBudgetManager.Services.Accounts.Domain.Accounts;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

public sealed class CreateAccountCommandHandler(
    IEventsStorage eventsStorage,
    IMediator mediator) : CommandHandlerBase<CreateAccountCommand>
{
    private readonly IEventsStorage eventsStorage = eventsStorage;

    private readonly IMediator mediator = mediator;

    protected override async Task<OneOf<CommandResults.Success, CommandResults.Failure>> Handle(
        CreateAccountCommand command,
        CancellationToken cancellationToken)
    {
        var accountType = await this.GetAccountType(command.AccountTypeId, cancellationToken);
        if (accountType is null)
        {
            return new CommandResults.Failure($"Account type with id {command.AccountTypeId} not found.");
        }

        var account = AccountAggregate.Create(
            command.Id,
            command.Name,
            DateTimeOffset.UtcNow,
            accountType.Value,
            command.Description,
            command.Currency,
            command.OwnerId);
        await this.eventsStorage.Store(account.AggregateName, account.AggregateId, account.GetUncommittedEvents(), cancellationToken);
        return new CommandResults.Success();
    }

    private async Task<AccountType?> GetAccountType(int accountTypeId, CancellationToken cancellationToken)
    {
        var accountTypesQuery = new FetchAccountTypesQuery();
        var accountTypes = await this.mediator.Fetch(accountTypesQuery, cancellationToken);
        var result = accountTypes.AccountTypes.FirstOrDefault(at => at.Id == accountTypeId);
        if (result == default)
        {
            return null;
        }

        return result;
    }
}
