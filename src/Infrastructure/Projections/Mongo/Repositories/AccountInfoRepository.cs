namespace HomeBudgetManager.Services.Accounts.Infrastructure.Projections.Mongo.Repositories;

using HomeBudgetManager.Services.Accounts.Domain.Accounts;
using HomeBudgetManager.Services.Accounts.Domain.Accounts.Projections;
using HomeBudgetManager.Services.Accounts.Domain.Contractors;

using Microsoft.Extensions.Logging;

using MongoDB.Driver;

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

    public async Task<IReadOnlyCollection<AccountInfo>> FindAccounts(CancellationToken cancellationToken)
    {
        var collection = this.collectionProvider.Accounts;
        var filter = Builders<AccountInfoDto>.Filter.Eq(p => p.OwnerId, null);
        var searchOptions = new FindOptions<AccountInfoDto>()
        {
        };
        var itemsMatched = await collection.FindAsync(filter, searchOptions, cancellationToken)
            .ConfigureAwait(false);
        var items = await itemsMatched
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        if (items.Count == 0)
        {
            return Array.Empty<AccountInfo>();
        }

        return items.Select(Map).ToList();
    }

    public async Task<AccountInfo?> GetAccount(Guid accountId, CancellationToken cancelationToken)
    {
        var collection = this.collectionProvider.Accounts;
        var filter = Builders<AccountInfoDto>.Filter.Eq(p => p.Id, accountId);
        var searchOptions = new FindOptions<AccountInfoDto>()
        {
            Limit = 1
        };
        var itemsMatched = await collection.FindAsync(filter, searchOptions, cancelationToken)
            .ConfigureAwait(false);
        var items = await itemsMatched
            .ToListAsync(cancelationToken)
            .ConfigureAwait(false);
        if (items.Count == 0)
        {
            return null;
        }

        return items.Select(Map).Single();
    }

    private static AccountInfoDto Map(AccountInfo accountInfo)
    {
        return new AccountInfoDto
        {
            Id = accountInfo.Id.ToGuid(),
            Name = accountInfo.Name,
            CreationDate = accountInfo.CreationDate,
            LastModificationDate = accountInfo.LastModificationDate,
            Description = accountInfo.Description,
            AccountType = new AccountInfoDto.AccountTypeDto
            {
                Id = (int)accountInfo.AccountType.Id,
                Name = accountInfo.AccountType.Name,
            },
            CurrentBalance = accountInfo.CurrentBalance,
            Currency = accountInfo.Currency,
            OwnerId = accountInfo.OwnerId?.Value,
            IsActive = accountInfo.IsActive,
        };
    }

    private static AccountInfo Map(AccountInfoDto accountInfoDto)
    {
        return new AccountInfo(
            new AccountId(accountInfoDto.Id),
            accountInfoDto.Name,
            accountInfoDto.CreationDate,
            accountInfoDto.LastModificationDate,
            accountInfoDto.Description,
            new AccountType(
                accountInfoDto.AccountType.Id,
                accountInfoDto.AccountType.Name),
            accountInfoDto.CurrentBalance,
            accountInfoDto.Currency,
            accountInfoDto.OwnerId.HasValue ? new ContractorId(accountInfoDto.OwnerId.Value) : null,
            accountInfoDto.IsActive);
    }
}
