namespace HomeBudgetManager.Services.Accounts.Sdk.Dictionaries;

using System.Text.Json;

public sealed class DictionariesClient(
    HttpClient client,
    Func<IRequestBuilder> requestBuilderFactory) : IDictionariesClient
{
    private readonly HttpClient client = client;

    private readonly Func<IRequestBuilder> requestBuilderFactory = requestBuilderFactory;

    public async Task<ResponseWrapper<CreateAccountTypeResponse>> CreateAccountType(CreateAccountTypeRequest request, CancellationToken cancellationToken)
    {
        var apiRequest = this.requestBuilderFactory()
            .WithTarget("/api/dictionaries/account-types", HttpMethod.Post)
            .WithBody(request.Name)
            .Build();
        var apiResponse = await this.client.SendAsync(apiRequest, cancellationToken);
        if (apiResponse.IsSuccessStatusCode)
        {
            return new ResponseWrapper<CreateAccountTypeResponse>(new CreateAccountTypeResponse());
        }
        else if ((int)apiResponse.StatusCode >= 400 && (int)apiResponse.StatusCode < 500)
        {
            return new ResponseWrapper<CreateAccountTypeResponse>(Results.CreateUserError("InvalidInput"));
        }
        else
        {
            return new ResponseWrapper<CreateAccountTypeResponse>(Results.CreateServerError("ServerError"));
        }
    }

    public async Task<ResponseWrapper<UpdateAccountTypeResponse>> UpdateAccountType(UpdateAccountTypeRequest request, CancellationToken cancellationToken)
    {
        var apiRequest = this.requestBuilderFactory()
            .WithTarget($"/api/dictionaries/account-types/{request.AccountData.Id}", HttpMethod.Put)
            .WithBody(request.AccountData.Name)
            .Build();
        var apiResponse = await this.client.SendAsync(apiRequest, cancellationToken);
        if (apiResponse.IsSuccessStatusCode)
        {
            return new ResponseWrapper<UpdateAccountTypeResponse>(new UpdateAccountTypeResponse());
        }
        else if ((int)apiResponse.StatusCode >= 400 && (int)apiResponse.StatusCode < 500)
        {
            return new ResponseWrapper<UpdateAccountTypeResponse>(Results.CreateUserError("InvalidInput"));
        }
        else
        {
            return new ResponseWrapper<UpdateAccountTypeResponse>(Results.CreateServerError("ServerError"));
        }
    }

    public async Task<ResponseWrapper<GetAccountTypesResponse>> GetAccountTypes(GetAccountTypesRequest request, CancellationToken cancellationToken)
    {
        var apiRequest = this.requestBuilderFactory()
            .WithTarget("/api/dictionaries/account-types", HttpMethod.Get)
            .Build();
        var apiResponse = await this.client.SendAsync(apiRequest, cancellationToken);
        if (apiResponse.IsSuccessStatusCode)
        {
            var contents = await apiResponse.Content.ReadAsStringAsync(cancellationToken);
            var payload = JsonSerializer.Deserialize<Payloads.DictionaryItem[]>(contents, SerializationOptionsProvider.GetJsonSerializerOptions());
            if (payload is null)
            {
                return new ResponseWrapper<GetAccountTypesResponse>(new GetAccountTypesResponse([]));
            }

            var accountTypes = payload.Select(p => new AccountType(p.Id, p.DisplayName)).ToList();
            return new ResponseWrapper<GetAccountTypesResponse>(new GetAccountTypesResponse([.. accountTypes]));
        }
        else if ((int)apiResponse.StatusCode >= 400 && (int)apiResponse.StatusCode < 500)
        {
            return new ResponseWrapper<GetAccountTypesResponse>(Results.CreateUserError("InvalidInput"));
        }
        else
        {
            return new ResponseWrapper<GetAccountTypesResponse>(Results.CreateServerError("ServerError"));
        }
    }
}
