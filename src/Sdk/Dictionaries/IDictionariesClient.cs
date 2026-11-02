namespace HomeBudgetManager.Services.Accounts.Sdk.Dictionaries;

public interface IDictionariesClient
{
    Task<ResponseWrapper<CreateAccountTypeResponse>> CreateAccountTypeAsync(
        CreateAccountTypeRequest request,
        CancellationToken cancellationToken);

    Task<ResponseWrapper<GetAccountTypesResponse>> GetAccountTypesAsync(
        GetAccountTypesRequest request,
        CancellationToken cancellationToken);
}
