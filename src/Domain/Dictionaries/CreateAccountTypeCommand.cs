namespace HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

public sealed class CreateAccountTypeCommand(
    string name) : ICommand
{
    public string Name { get; } = name;
}
