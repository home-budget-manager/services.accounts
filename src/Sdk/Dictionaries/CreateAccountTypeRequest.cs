namespace HomeBudgetManager.Services.Accounts.Sdk.Dictionaries;

public sealed class CreateAccountTypeRequest(
    string name)
{
    public string Name { get; } = name;
}
