namespace HomeBudgetManager.Services.Accounts.Sdk.Dictionaries.Payloads;

public sealed class DictionaryItem(
    int id,
    string displayName)
{
    public int Id { get; } = id;

    public string DisplayName { get; } = displayName;
}
