namespace HomeBudgetManager.Services.Accounts.Sdk.UserAccounts;

public interface IUserAccountsClient
{
    Task<ResponseWrapper<CreateAccountResponse>> Create(
        CreateAccountRequest request,
        CancellationToken cancellationToken);

    Task<ResponseWrapper<GetUserAccountsResponse>> GetUserAccounts(
        GetUserAccountsRequest request,
        CancellationToken cancellationToken);
}
