namespace HomeBudgetManager.Services.Accounts.Sdk.Dictionaries;

using System.Collections.ObjectModel;

public sealed class GetAccountTypesResponse(
    Collection<AccountType> accountTypes)
{
    public Collection<AccountType> AccountTypes { get; } = accountTypes;
}
