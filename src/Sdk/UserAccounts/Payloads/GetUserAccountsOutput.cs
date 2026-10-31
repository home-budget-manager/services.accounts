namespace HomeBudgetManager.Services.Accounts.Sdk.UserAccounts.Payloads;

internal sealed class GetUserAccountsOutput(
    GetUserAccountsOutput.Item[] accounts)
{
    public Item[] Accounts { get; } = accounts;

    internal sealed class Item(
        Guid accountId,
        string name,
        GetUserAccountsOutput.Item.AccType accountType,
        decimal balance,
        decimal currentPeriodChange,
        string currency,
        bool isActive)
    {
        public Guid AccountId { get; } = accountId;

        public string Name { get; } = name;

        public AccType AccountType { get; } = accountType;

        public decimal Balance { get; } = balance;

        public decimal CurrentPeriodChange { get; } = currentPeriodChange;

        public string Currency { get; } = currency;

        public bool IsActive { get; } = isActive;

        internal sealed class AccType(
            int id,
            string name)
        {
            public int Id { get; } = id;
            public string Name { get; } = name;
        }
    } 
}
