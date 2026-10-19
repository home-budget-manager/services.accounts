namespace HomeBudgetManager.Services.Accounts.Application.Accounts.Projections.Handlers;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;
using HomeBudgetManager.Services.Accounts.Domain.Accounts.AggregateEvents;
using HomeBudgetManager.Services.Accounts.Domain.Accounts.Projections;

public sealed class AccountCreatedProjectionHandler(
    IAccountInfoRepository accountInfoRepository) : ProjectionHandlerBase<AccountCreated>
{
    private readonly IAccountInfoRepository accountInfoRepository = accountInfoRepository;

    protected override async Task Handle(AccountCreated @event, CancellationToken cancellationToken)
    {
        var accountInfo = new AccountInfo(
            @event.Id,
            @event.Name,
            @event.CreationDate,
            @event.CreationDate,
            @event.Description,
            @event.AccountType,
            0,
            @event.Currency,
            @event.OwnerId,
            true);
        await this.accountInfoRepository.InsertAccount(accountInfo, cancellationToken);
    }
}
