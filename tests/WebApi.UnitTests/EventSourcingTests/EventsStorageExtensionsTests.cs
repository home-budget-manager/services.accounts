namespace HomeBudgetManager.Services.Accounts.WebApi.UnitTests.EventSourcingTests;

using HomeBudgetManager.Services.Accounts.Application.EventSourcing;
using HomeBudgetManager.Services.Accounts.Domain;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries;
using HomeBudgetManager.Services.Accounts.Domain.Dictionaries.AggregateEvents;

public sealed class EventsStorageExtensionsTests
{
    [Fact]
    public async Task Store_UsesTheLoadedAggregateVersion()
    {
        var aggregate = DictionariesAggregate.CreateFromEvents(
        [
            new DictionariesCreated(Guid.Empty),
            new AccountTypeAdded(Guid.Empty, 1, "Checking")
        ]);
        aggregate.AddAccountType(2, "Savings");

        var eventsStorage = new Mock<IEventsStorage>();
        var cancellationToken = CancellationToken.None;

        await eventsStorage.Object.Store(aggregate, cancellationToken);

        eventsStorage.Verify(storage => storage.Store(
            nameof(DictionariesAggregate),
            Guid.Empty,
            It.IsAny<IEnumerable<IAggregateEvent>>(),
            2,
            cancellationToken), Times.Once);
    }
}
