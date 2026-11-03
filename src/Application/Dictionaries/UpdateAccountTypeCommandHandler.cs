namespace HomeBudgetManager.Services.Accounts.Application.Dictionaries;

using HomeBudgetManager.Services.Accounts.Application.EventSourcing;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

public sealed class UpdateAccountTypeCommandHandler(
    IEventsStorage eventsStorage) : CommandHandlerBase<UpdateAccountTypeCommand>
{
    private readonly IEventsStorage eventsStorage = eventsStorage;

    protected override async Task<OneOf<CommandResults.Success, CommandResults.Failure>> Handle(
        UpdateAccountTypeCommand command,
        CancellationToken cancellationToken)
    {
        var dictionariesAggregate = await this.eventsStorage.Load(
            typeof(DictionariesAggregate).Name,
            Guid.Empty,
            DictionariesAggregate.CreateFromEvents,
            cancellationToken);
        dictionariesAggregate ??= DictionariesAggregate.Create();
        if (!dictionariesAggregate.AccountTypes.Any(at => at.Id == command.Id))
        {
            return new CommandResults.Failure("AccountTypeNotFound");
        }

        dictionariesAggregate.UpdateAccountType(command.Id, command.DisplayName);
        await this.eventsStorage.Store(dictionariesAggregate, cancellationToken);
        return new CommandResults.Success();
    }
}
