namespace HomeBudgetManager.Services.Accounts.Application.Dictionaries;

using HomeBudgetManager.Services.Accounts.Domain.Dictionaries.AggregateEvents;

public sealed class CacheInvalidationHandler(
    IDictionariesCache dictionariesCache)
    : ProjectionHandlerBase<DictionariesCreated>
{
    private readonly IDictionariesCache dictionariesCache = dictionariesCache;

    protected override Task Handle(DictionariesCreated @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
