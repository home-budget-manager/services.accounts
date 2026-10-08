namespace HomeBudgetManager.Services.Accounts.Application.Accounts;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;

public sealed class CreateAccountCommandHandler : CommandHandlerBase<CreateAccountCommand>
{
    protected override async Task<OneOf<CommandResults.Success, CommandResults.Failure>> Handle(
        CreateAccountCommand command,
        CancellationToken cancellationToken)
    {
        await Task.Yield();
        var account = AccountAggregate.Create(
            command.Id,
            command.Name,
            DateTimeOffset.UtcNow,
            command.AccountType,
            command.Description,
            command.Currency,
            command.OwnerId);
        WriteModel.AddAccountEvents(account.GetUncommittedEvents());
        return new CommandResults.Success();
    }
}
