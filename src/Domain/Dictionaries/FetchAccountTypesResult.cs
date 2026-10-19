namespace HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

using System.Collections.ObjectModel;

public sealed class FetchAccountTypesResult(IEnumerable<AccountType> accountTypes) : IResult
{
    public ReadOnlyCollection<AccountType> AccountTypes { get; } = new([.. accountTypes]);
}
