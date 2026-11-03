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

        if (accountTypes.Any(at => at.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"Account type with name '{name}' already exists.");
        }

        var @event = new AccountTypeAdded(this.AggregateId, id, name);
        this.ApplyEvent(@event);
    }

    public void UpdateAccountType(int id, string newName)
    {
        var accountTypeExists = accountTypes.Count(at => at.Id == id) == 1;
        if (!accountTypeExists)
        {
            throw new InvalidOperationException($"Account type with ID {id} does not exist.");
        }

        if (accountTypes.Any(at => at.Name.Equals(newName, StringComparison.OrdinalIgnoreCase) && at.Id != id))
        {
            throw new InvalidOperationException($"Account type with name '{newName}' already exists.");
        }

        var @event = new AccountTypeChanged(this.AggregateId, id, newName);
        this.ApplyEvent(@event);
    }

    protected override void When(IAggregateEvent @event)
    {
        switch (@event)
        {
            case DictionariesCreated dictionariesCreated:
                this.When(dictionariesCreated);
                break;
            case AccountTypeAdded accountTypeAdded:
                this.When(accountTypeAdded);
                break;
            case AccountTypeChanged accountTypeChanged:
                this.When(accountTypeChanged);
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

    private void When(AccountTypeChanged @event)
    {
        var currentAccountType = accountTypes.Single(at => at.Id == @event.Id);
        var accountType = accountTypes.IndexOf(currentAccountType);
        var newAccountType = new AccountType(@event.Id, @event.Name);
        accountTypes[accountType] = newAccountType;
    }
}
