namespace HomeBudgetManager.Services.Accounts.Application.Accounts;

using HomeBudgetManager.Services.Accounts.Application.EventSourcing;
using HomeBudgetManager.Services.Accounts.Domain.Accounts;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

public sealed class CreateAccountCommandHandler(
    IEventsStorage eventsStorage) : CommandHandlerBase<CreateAccountCommand>
{
    private readonly IEventsStorage eventsStorage = eventsStorage;

    protected override async Task<OneOf<CommandResults.Success, CommandResults.Failure>> Handle(
        CreateAccountCommand command,
        CancellationToken cancellationToken)
    {
        var accountType = AccountType.FromId(command.AccountTypeId);
        var account = AccountAggregate.Create(
            command.Id,
            command.Name,
            DateTimeOffset.UtcNow,
            accountType,
            command.Description,
            command.Currency,
            command.OwnerId);
        await this.eventsStorage.Store(account.AggregateName, account.AggregateId, account.GetUncommittedEvents(), cancellationToken);
        return new CommandResults.Success();
    }
}
