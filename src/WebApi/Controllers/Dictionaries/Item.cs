namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers.Dictionaries;

public sealed class Item(
    int id,
    string displayName)
{
    public int Id { get; } = id;

    public string DisplayName { get; } = displayName;
}
