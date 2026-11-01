namespace HomeBudgetManager.Services.Accounts.Sdk;

public sealed class ResponseWrapper<TResponse>
    where TResponse : class
{
    public ResponseWrapper(TResponse response)
    {
        this.Response = response;
    }

    public ResponseWrapper(Results.UserError userError)
    {
        this.Response = userError;
    }

    public ResponseWrapper(Results.ServerError serverError)
    {
        this.Response = serverError;
    }

    public OneOf<TResponse, Results.UserError, Results.ServerError> Response { get; }
}
