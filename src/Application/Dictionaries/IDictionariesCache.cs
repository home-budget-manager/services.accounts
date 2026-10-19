namespace HomeBudgetManager.Services.Accounts.Application.Dictionaries;

using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

public interface IDictionariesCache
{
    Task<DictionariesAggregate> GetDictionaries(CancellationToken cancellationToken);

    void Invalidate();
}
