namespace HomeBudgetManager.Services.Accounts.WebApi.Controllers.Dictionaries;

public class Item(
    int id,
    string displayName)
{
    public int Id { get; } = id;

    public string DisplayName { get; } = displayName;
}
