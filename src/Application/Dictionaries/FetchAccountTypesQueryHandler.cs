namespace HomeBudgetManager.Services.Accounts.Application.Dictionaries;

using HomeBudgetManager.Services.Accounts.Application.EventSourcing;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

public sealed class FetchAccountTypesQueryHandler(
    IDictionariesCache dictionariesCache) : QueryHandlerBase<FetchAccountTypesQuery, FetchAccountTypesResult>
{
    private readonly IDictionariesCache dictionariesCache = dictionariesCache;

    protected override async Task<FetchAccountTypesResult> Handle(
        FetchAccountTypesQuery query,
        CancellationToken cancellationToken)
    {
        var dictionariesAggregate = await this.dictionariesCache.GetDictionaries(cancellationToken);
        if (dictionariesAggregate is null)
        {
            return new([]);
        }

        return new(dictionariesAggregate.AccountTypes);
    }
}
