namespace HomeBudgetManager.Services.Accounts.Sdk;

public sealed class RequestBuilder : IRequestBuilder
{
    private string route = string.Empty;

    private HttpMethod method;

    private object? body;

    public IRequestBuilder WithTarget(string route, HttpMethod method)
    {
        this.route = route;
        this.method = method;
        return this;
    }

    public IRequestBuilder WithBody(object body)
    {
        this.body = body;
        return this;
    }

    public HttpRequestMessage Build()
    {
        var result = new HttpRequestMessage(this.method, this.route);
        if (this.body is not null)
        {
            result.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(this.body), System.Text.Encoding.UTF8, "application/json");
        }

        return result;
    }
}
