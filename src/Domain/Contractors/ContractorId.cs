namespace HomeBudgetManager.Services.Accounts.Domain.Contractors;

public readonly struct ContractorId
{
    [System.Text.Json.Serialization.JsonConstructor]
    public ContractorId(Guid value)
    {
        this.Value = value;
    }

    public Guid Value { get; }

    public static ContractorId Empty => new(Guid.Empty);

    public bool IsEmpty => this.Value == Guid.Empty;

    public static ContractorId New() => new(Guid.NewGuid());

    public Guid ToGuid() => this.Value;

    public override string ToString() => this.Value.ToString();
}
