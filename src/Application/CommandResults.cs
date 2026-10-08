namespace HomeBudgetManager.Services.Accounts.Application;

public sealed class CommandResults
{
    public sealed class Success { }

    public sealed class Failure(string errorMessage)
    {
        public string ErrorMessage { get; } = errorMessage;
    }
}
