namespace HomeBudgetManager.Services.Accounts.Application;

using System.Collections.ObjectModel;

using HomeBudgetManager.Services.Accounts.Domain;

internal static class WriteModel
{
    private static readonly Collection<IAggregateEvent> AccountsEvents = [];

    public static void AddAccountEvent(IAggregateEvent @event)
    {
        AccountsEvents.Add(@event);
    }

    public static void AddAccountEvents(IEnumerable<IAggregateEvent> events)
    {
        var materialized = events.ToList();
        foreach (var @event in materialized)
        {
            AccountsEvents.Add(@event);
        }

        var aggregates = materialized.Select(e => e.AggregateId).Distinct().ToList();
        foreach (var aggregateId in aggregates)
        {
            UpdateAccountReadModel(aggregateId);
        }
    }

    public static IEnumerable<IAggregateEvent> GetAccountEvents(Guid aggregateId)
    {
        return [.. AccountsEvents.Where(e => e.AggregateId == aggregateId)];
    }

    private static void UpdateAccountReadModel(Guid aggregateId)
    {
        var events = GetAccountEvents(aggregateId);
        var accountAggregate = Domain.Accounts.AccountAggregate.CreateFromEvents(events);
        var accountInfo = new Accounts.ReadModel.AccountInfo(
            accountAggregate.Id,
            accountAggregate.Name,
            accountAggregate.AccountType,
            accountAggregate.Description,
            accountAggregate.Currency,
            accountAggregate.OwnerId);
        ReadModel.UpsertAccount(accountInfo);
    }
}
