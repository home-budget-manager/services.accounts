namespace HomeBudgetManager.Services.Accounts.Sdk;

public static class Results
{
    public static UserError CreateUserError(string message)
    {
        return new UserError(message);
    }

    public static ServerError CreateServerError(string message)
    {
        return new ServerError(message);
    }

    public sealed class UserError
    {
        public UserError(string message)
        {
            this.Message = message;
        }

        public string Message { get; }
    }

    public sealed class ServerError
    {
        public ServerError(string message)
        {
            this.Message = message;
        }

        public string Message { get; }
    }
}
