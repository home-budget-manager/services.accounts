namespace HomeBudgetManager.Services.Accounts.Application.Dictionaries.CacheInvalidation;

using HomeBudgetManager.Services.Accounts.Domain.Dictionaries.AggregateEvents;

public sealed class DictionariesCreatedProjectionHandler(
    IDictionariesCache dictionariesCache) : ProjectionHandlerBase<DictionariesCreated>
{
    private readonly IDictionariesCache dictionariesCache = dictionariesCache;

    protected override Task Handle(DictionariesCreated @event, CancellationToken cancellationToken)
    {
        this.dictionariesCache.Invalidate();
        return Task.CompletedTask;
    }
}
