namespace HomeBudgetManager.Services.Accounts.Sdk;

public interface IRequestBuilder
{
    IRequestBuilder WithTarget(string route, HttpMethod method);

    IRequestBuilder WithBody(object body);

    HttpRequestMessage Build();
}
