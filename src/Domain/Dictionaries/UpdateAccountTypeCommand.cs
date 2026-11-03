namespace HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

public sealed class UpdateAccountTypeCommand(
    int id,
    string displayName) : ICommand
{
    public int Id { get; } = id;

    public string DisplayName { get; } = displayName;
}
