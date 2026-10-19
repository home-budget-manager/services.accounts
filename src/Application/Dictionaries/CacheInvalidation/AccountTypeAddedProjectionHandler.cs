namespace HomeBudgetManager.Services.Accounts.Application.Dictionaries.CacheInvalidation;

using HomeBudgetManager.Services.Accounts.Domain.Dictionaries.AggregateEvents;

public sealed class AccountTypeAddedProjectionHandler(
    IDictionariesCache dictionariesCache) : ProjectionHandlerBase<AccountTypeAdded>
{
    private readonly IDictionariesCache dictionariesCache = dictionariesCache;

    protected override Task Handle(AccountTypeAdded @event, CancellationToken cancellationToken)
    {
        this.dictionariesCache.Invalidate();
        return Task.CompletedTask;
    }
}
