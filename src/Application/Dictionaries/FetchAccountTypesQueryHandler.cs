namespace HomeBudgetManager.Services.Accounts.Application.Dictionaries;

using HomeBudgetManager.Services.Accounts.Application.EventSourcing;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

public sealed class FetchAccountTypesQueryHandler(
    IEventsStorage eventsStorage) : QueryHandlerBase<FetchAccountTypesQuery, FetchAccountTypesResult>
{
    private readonly IEventsStorage eventsStorage = eventsStorage;

    protected override async Task<FetchAccountTypesResult> Handle(
        FetchAccountTypesQuery query,
        CancellationToken cancellationToken)
    {
        var dictionariesAggregate = await this.eventsStorage.Load(
            typeof(DictionariesAggregate).Name,
            Guid.Empty,
            DictionariesAggregate.CreateFromEvents,
            cancellationToken);
        if (dictionariesAggregate is null)
        {
            return new([]);
        }

        return new(dictionariesAggregate.AccountTypes);
    }
}
