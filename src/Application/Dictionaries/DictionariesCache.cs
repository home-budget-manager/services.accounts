namespace HomeBudgetManager.Services.Accounts.Application.Dictionaries;

using HomeBudgetManager.Services.Accounts.Application.EventSourcing;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

using Microsoft.Extensions.Caching.Memory;

public sealed class DictionariesCache(
    IEventsStorage eventsStorage,
    IMemoryCache cache) : IDictionariesCache
{
    private const string CacheKey = "DictionariesAggregate";

    private readonly IEventsStorage eventsStorage = eventsStorage;

    private readonly IMemoryCache cache = cache;

    public async Task<DictionariesAggregate> GetDictionaries(CancellationToken cancellationToken)
    {
        var result = await this.cache.GetOrCreateAsync(
            CacheKey,
            async entry =>
            {
                var dictionaries = await this.eventsStorage.Load(
                    typeof(DictionariesAggregate).Name,
                    Guid.Empty,
                    DictionariesAggregate.CreateFromEvents,
                    cancellationToken);
                if (dictionaries is null)
                {
                    entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(-1);
                }
                else
                {
                    entry.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(5);
                }

                return dictionaries;
            });
        return result ?? DictionariesAggregate.Create();
    }

    public void Invalidate()
    {
        this.cache.Remove(CacheKey);
    }
}
