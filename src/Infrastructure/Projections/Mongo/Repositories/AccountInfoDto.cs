namespace HomeBudgetManager.Services.Accounts.Infrastructure.Projections.Mongo.Repositories;

using MongoDB.Bson.Serialization.Attributes;

public sealed class AccountInfoDto
{
    [BsonId]
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTimeOffset CreationDate { get; set; }

    public DateTimeOffset LastModificationDate { get; set; }

    public string Description { get; set; } = string.Empty;

    public AccountTypeDto AccountType { get; set; } = default!;

    public decimal CurrentBalance { get; set; }

    public string Currency { get; set; } = string.Empty;

    public Guid? OwnerId { get; set; }

    public bool IsActive { get; set; }

    public sealed class AccountTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
