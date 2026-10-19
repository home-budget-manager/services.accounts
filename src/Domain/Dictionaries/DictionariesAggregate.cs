namespace HomeBudgetManager.Services.Accounts.Domain.Dictionaries;

using System.Collections.ObjectModel;

using HomeBudgetManager.Services.Accounts.Domain.Contractors;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries.AggregateEvents;

public sealed class DictionariesAggregate : AggregateRootBase
{
    private readonly Collection<AccountType> accountTypes = [];

    private DictionariesAggregate()
    {
    }

    public override Guid AggregateId => this.Id;

    public Guid Id { get; private set; }

    public IReadOnlyCollection<AccountType> AccountTypes => accountTypes;

    public static DictionariesAggregate Create()
    {
        var dictionaries = new DictionariesAggregate();
        var @event = new DictionariesCreated(Guid.Empty);
        dictionaries.ApplyEvent(@event);
        return dictionaries;
    }


    public static DictionariesAggregate CreateFromEvents(IEnumerable<IAggregateEvent> events)
    {
        var dictionaries = new DictionariesAggregate();
        dictionaries.ApplyEventsWithoutRecording(events);
        return dictionaries;
    }

    public void AddAccountType(int id, string name)
    {
        if (accountTypes.Any(at => at.Id == id))
        {
            throw new InvalidOperationException($"Account type with ID {id} already exists.");
        }

        var @event = new AccountTypeAdded(this.AggregateId, id, name);
        this.ApplyEvent(@event);
    }

    protected override void When(IAggregateEvent @event)
    {
        switch(@event)
        {
            case DictionariesCreated dictionariesCreated:
                this.When(dictionariesCreated);
                break;
            case AccountTypeAdded accountTypeAdded:
                this.When(accountTypeAdded);
                break;
            default:
                throw new InvalidOperationException($"Unhandled event type: {@event.GetType().Name}");
        }
    }

    private void When(DictionariesCreated @event)
    {
        this.Id = @event.AggregateId;
    }

    private void When(AccountTypeAdded @event)
    {
        var accountType = new AccountType(@event.Id, @event.Name);
        accountTypes.Add(accountType);
    }
}
