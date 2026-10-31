namespace HomeBudgetManager.Services.Accounts.Sdk.UserAccounts;

using System.Text.Json;

public sealed class UserAccountsClient(
    HttpClient client,
    Func<IRequestBuilder> requestBuilderFactory) : IUserAccountsClient
{
    private readonly HttpClient client = client;

    private readonly Func<IRequestBuilder> requestBuilderFactory = requestBuilderFactory;

    public async Task<ResponseWrapper<CreateAccountResponse>> Create(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        var apiRequest = requestBuilderFactory()
            .WithTarget("/api/useraccounts", HttpMethod.Post)
            .WithBody(request)
            .Build();
        var apiResponse = await client.SendAsync(apiRequest, cancellationToken);
        if (apiResponse.IsSuccessStatusCode)
        {
            var responseContent = await apiResponse.Content.ReadAsStringAsync(cancellationToken);
            var createAccountOutput = JsonSerializer.Deserialize<Payloads.CreateAccountOutput>(responseContent, SerializationOptionsProvider.GetJsonSerializerOptions());
            if (createAccountOutput is null)
            {
                return new ResponseWrapper<CreateAccountResponse>(Results.CreateServerError("ServerError"));
            }

            return new ResponseWrapper<CreateAccountResponse>(new CreateAccountResponse(createAccountOutput.AccountId));
        }
        else if ((int)apiResponse.StatusCode >= 400 && (int)apiResponse.StatusCode < 500)
        {
            ////var responseContent = await apiResponse.Content.ReadAsStringAsync(cancellationToken);
            return new ResponseWrapper<CreateAccountResponse>(Results.CreateUserError("InvalidInput"));
        }
        else
        {
            ////var responseContent = await apiResponse.Content.ReadAsStringAsync(cancellationToken);
            return new ResponseWrapper<CreateAccountResponse>(Results.CreateServerError("ServerError"));
        }
    }

    public async Task<ResponseWrapper<GetUserAccountsResponse>> GetUserAccounts(
        GetUserAccountsRequest request,
        CancellationToken cancellationToken)
    {
        var apiRequest = requestBuilderFactory()
            .WithTarget("/api/useraccounts", HttpMethod.Get)
            .Build();
        var apiResponse = await client.SendAsync(apiRequest, cancellationToken);
        if (apiResponse.IsSuccessStatusCode)
        {
            var responseContent = await apiResponse.Content.ReadAsStringAsync(cancellationToken);
            var getUserAccountsOutput = JsonSerializer.Deserialize<Payloads.GetUserAccountsOutput>(responseContent);
            if (getUserAccountsOutput is null)
            {
                return new ResponseWrapper<GetUserAccountsResponse>(Results.CreateServerError("ServerError"));
            }
            var userAccounts = getUserAccountsOutput.Accounts.Select(account => new GetUserAccountsResponse.Account(
                account.AccountId,
                account.Name,
                new GetUserAccountsResponse.Account.AccType(account.AccountType.Id, account.AccountType.Name),
                account.Currency,
                account.Balance)).ToArray();
            return new ResponseWrapper<GetUserAccountsResponse>(new GetUserAccountsResponse(userAccounts));
        }
        else if ((int)apiResponse.StatusCode >= 400 && (int)apiResponse.StatusCode < 500)
        {
            ////var responseContent = await apiResponse.Content.ReadAsStringAsync(cancellationToken);
            return new ResponseWrapper<GetUserAccountsResponse>(Results.CreateUserError("InvalidInput"));
        }
        else
        {
            ////var responseContent = await apiResponse.Content.ReadAsStringAsync(cancellationToken);
            return new ResponseWrapper<GetUserAccountsResponse>(Results.CreateServerError("ServerError"));
        }
    }
}
