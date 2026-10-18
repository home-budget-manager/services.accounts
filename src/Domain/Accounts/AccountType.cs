namespace HomeBudgetManager.Services.Accounts.Domain.Accounts;

public readonly record struct AccountType(int Id, string Name)
{
    public static readonly AccountType Cash = new(1, "Cash");

    public static readonly AccountType Savings = new(2, "Savings");

    public static readonly AccountType CreditCard = new(3, "Credit card");

    public static readonly AccountType CashWallet = new(4, "Cash wallet");

    private static readonly IReadOnlyCollection<AccountType> All = new List<AccountType>
    {
        Cash,
        Savings,
        CreditCard,
        CashWallet
    }.AsReadOnly();

    public static AccountType FromId(int id)
    {
        return All.Single(at => at.Id == id);
    }
}