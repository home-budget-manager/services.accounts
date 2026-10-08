namespace HomeBudgetManager.Services.Accounts.Application;

using System.Collections.ObjectModel;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;

internal static class ReadModel
{
    private static readonly Collection<Accounts.ReadModel.AccountInfo> Accounts = [];

    public static void UpsertAccount(Accounts.ReadModel.AccountInfo accountInfo)
    {
        var existingAccount = Accounts.FirstOrDefault(a => a.Id == accountInfo.Id);
        if (existingAccount is not null)
        {
            Accounts.Remove(existingAccount);
        }

        Accounts.Add(accountInfo);
    }

    public static Accounts.ReadModel.AccountInfo? GetAccountById(AccountId accountId)
    {
        return Accounts.FirstOrDefault(a => a.Id == accountId);
    }
}
