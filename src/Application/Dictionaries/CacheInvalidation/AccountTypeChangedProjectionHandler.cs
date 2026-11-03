namespace HomeBudgetManager.Services.Accounts.Application.Dictionaries.CacheInvalidation;

using HomeBudgetManager.Services.Accounts.Domain.Dictionaries.AggregateEvents;

public sealed class AccountTypeChangedProjectionHandler(
    IDictionariesCache dictionariesCache) : ProjectionHandlerBase<AccountTypeChanged>
{
    private readonly IDictionariesCache dictionariesCache = dictionariesCache;

    protected override Task Handle(AccountTypeChanged @event, CancellationToken cancellationToken)
    {
        this.dictionariesCache.Invalidate();
        return Task.CompletedTask;
    }
}
