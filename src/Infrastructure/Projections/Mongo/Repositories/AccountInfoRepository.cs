namespace HomeBudgetManager.Services.Accounts.Infrastructure.Projections.Mongo.Repositories;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;
using HomeBudgetManager.Services.Accounts.Domain.Accounts.Projections;

public sealed class AccountInfoRepository(
    ICollectionProvider collectionProvider) : IAccountInfoRepository
{
    private readonly ICollectionProvider collectionProvider = collectionProvider;

    public async Task InsertAccount(AccountInfo accountInfo, CancellationToken cancellationToken)
    {
        var collection = this.collectionProvider.Accounts;
        var accountInfoDto = Map(accountInfo);
        await collection.InsertOneAsync(
            accountInfoDto,
            null,
            cancellationToken);
    }

    private static AccountInfoDto Map(AccountInfo accountInfo)
    {
        return new AccountInfoDto
        {
            Id = accountInfo.Id.ToGuid(),
            Name = accountInfo.Name,
            CreationDate = accountInfo.CreationDate,
            Description = accountInfo.Description,
            AccountType = new AccountInfoDto.AccountTypeDto
            {
                Id = (int)accountInfo.AccountType.Id,
                Name = accountInfo.AccountType.Name,
            },
            Currency = accountInfo.Currency,
            OwnerId = accountInfo.OwnerId?.Value
        };
    }
}
