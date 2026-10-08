namespace HomeBudgetManager.Services.Accounts.Domain.Accounts;

using HomeBudgetManager.Services.Accounts.Domain.Accounts.AggregateEvents;
using HomeBudgetManager.Services.Accounts.Domain.Contractors;

public sealed class AccountAggregate : AggregateRootBase
{
    private AccountAggregate()
    {
    }

    public override Guid AggregateId => this.Id.ToGuid();

    public AccountId Id { get; private set; } = default;

    public string Name { get; private set; } = string.Empty;

    public DateTimeOffset CreationDate { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public AccountType AccountType { get; private set; } = default;

    public string Currency { get; private set; } = string.Empty;

    public ContractorId? OwnerId { get; private set; } = null;

    public static AccountAggregate Create(AccountId id, string name, DateTimeOffset creationDate, AccountType accountType, string description, string currency, ContractorId? ownerId)
    {
        var account = new AccountAggregate();
        var @event = new AccountCreated(id, name, creationDate, description, accountType, currency, ownerId);
        account.ApplyEvent(@event);
        return account;
    }

    public static AccountAggregate CreateFromEvents(IEnumerable<IAggregateEvent> events)
    {
        var account = new AccountAggregate();
        account.ApplyEventsWithoutRecording(events);
        return account;
    }

    protected override void When(IAggregateEvent @event)
    {
        switch (@event)
        {
            case AccountCreated e:
                When(e);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private void When(AccountCreated @event)
    {
        this.Id = @event.Id;
        this.Name = @event.Name;
        this.CreationDate = @event.CreationDate;
        this.AccountType = @event.AccountType;
        this.Description = @event.Description;
        this.Currency = @event.Currency;
        this.OwnerId = @event.OwnerId;
    }
}
