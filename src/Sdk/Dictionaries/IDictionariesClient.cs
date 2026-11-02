namespace HomeBudgetManager.Services.Accounts.Sdk.Dictionaries;

public interface IDictionariesClient
{
    Task<ResponseWrapper<CreateAccountTypeResponse>> CreateAccountType(
        CreateAccountTypeRequest request,
        CancellationToken cancellationToken);

    Task<ResponseWrapper<GetAccountTypesResponse>> GetAccountTypes(
        GetAccountTypesRequest request,
        CancellationToken cancellationToken);
}
