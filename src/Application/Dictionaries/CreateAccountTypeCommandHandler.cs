namespace HomeBudgetManager.Services.Accounts.Application.Dictionaries;

using HomeBudgetManager.Services.Accounts.Application.EventSourcing;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

public sealed class CreateAccountTypeCommandHandler(
    IEventsStorage eventsStorage)
    : CommandHandlerBase<CreateAccountTypeCommand>
{
    private readonly IEventsStorage eventsStorage = eventsStorage;

    protected override async Task<OneOf<CommandResults.Success, CommandResults.Failure>> Handle(
        CreateAccountTypeCommand command,
        CancellationToken cancellationToken)
    {
        var dictionariesAggregate = await this.eventsStorage.Load(
            typeof(DictionariesAggregate).Name,
            Guid.Empty,
            DictionariesAggregate.CreateFromEvents,
            cancellationToken);
        if (dictionariesAggregate == null)
        {
            dictionariesAggregate = DictionariesAggregate.Create();
        }

        var newId = 1;
        if (dictionariesAggregate.AccountTypes.Any())
        {
            newId = dictionariesAggregate.AccountTypes.Max(at => at.Id) + 1;
        }

        dictionariesAggregate.AddAccountType(newId, command.Name);
        var newEvents = dictionariesAggregate.GetUncommittedEvents();
        await this.eventsStorage.Store(dictionariesAggregate, cancellationToken);
        return new CommandResults.Success();
    }
}
