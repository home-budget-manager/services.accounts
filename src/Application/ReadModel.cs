namespace HomeBudgetManager.Services.Accounts.Application;

using System.Collections.ObjectModel;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;
using HomeBudgetManager.Services.Accounts.Domain.Accounts.Projections;

internal static class ReadModel
{
    private static readonly Collection<AccountInfo> Accounts = [];

    public static void UpsertAccount(AccountInfo accountInfo)
    {
        var existingAccount = Accounts.FirstOrDefault(a => a.Id == accountInfo.Id);
        if (existingAccount is not null)
        {
            Accounts.Remove(existingAccount);
        }

        Accounts.Add(accountInfo);
    }

    public static AccountInfo? GetAccountById(AccountId accountId)
    {
        return Accounts.FirstOrDefault(a => a.Id == accountId);
    }
}
