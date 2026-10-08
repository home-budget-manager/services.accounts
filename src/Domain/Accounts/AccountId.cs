namespace HomeBudgetManager.Services.Accounts.Domain.Accounts;

public readonly record struct AccountId(Guid Value)
{
    public static AccountId Empty => new(Guid.Empty);

    public bool IsEmpty => this.Value == Guid.Empty;

    public static AccountId New() => new(Guid.NewGuid());

    public Guid ToGuid() => this.Value;

    public override string ToString() => this.Value.ToString();
}
