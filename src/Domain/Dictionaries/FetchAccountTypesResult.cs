namespace HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

using System.Collections.ObjectModel;

public sealed class FetchAccountTypesResult : IResult
{
    public FetchAccountTypesResult(IEnumerable<AccountType> accountTypes)
    {
        AccountTypes = new ReadOnlyCollection<AccountType>([.. accountTypes]);
    }

    public ReadOnlyCollection<AccountType> AccountTypes { get; }
}
