namespace HomeBudgetManager.Services.Accounts.Sdk.UserAccounts;

using System.Collections.ObjectModel;

public sealed class GetUserAccountsResponse(
    IEnumerable<GetUserAccountsResponse.Account> accounts)
{
    public ReadOnlyCollection<GetUserAccountsResponse.Account> Accounts { get; } = new((accounts ?? []).ToList());

    public sealed class Account(
    Guid id,
    string name,
    Account.AccType accountType,
    string currency,
    decimal balance)
    {
        public Guid Id { get; } = id;

        public string Name { get; } = name;

        public AccType AccountType { get; } = accountType;

        public string Currency { get; } = currency;

        public decimal Balance { get; } = balance;

        public sealed class AccType(
            int id,
            string name)
        {
            public int Id { get; } = id;

            public string Name { get; } = name;
        }
    }
}