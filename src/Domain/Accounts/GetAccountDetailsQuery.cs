namespace HomeBudgetManager.Services.Accounts.Domain.Accounts;

public sealed class GetAccountDetailsQuery(
    AccountId id) : IQuery<GetAccountDetailsResult?>
{
    public AccountId Id { get; } = id;
}
