namespace HomeBudgetManager.Services.Accounts.WebApi.BackgroundServices;

using HomeBudgetManager.Services.Accounts.Infrastructure.EventsSourcing.Kurrent;
using HomeBudgetManager.Services.Accounts.WebApi.Settings;

using KurrentDB.Client;

using Grpc.Core;

using Microsoft.Extensions.Options;

public sealed class ReadModelHydration(
    IServiceScopeFactory serviceScopeFactory,
    IOptions<KurrentDbSettings> kurrentSettingsOptions,
    ILogger<ReadModelHydration> logger)
    : BackgroundService
{
    private KurrentDbSettings Settings => kurrentSettingsOptions.Value;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        await this.Configure(serviceProvider, stoppingToken);
        await this.StartHydration(serviceProvider, stoppingToken);
    }

    private async Task Configure(
        IServiceProvider serviceProvider,
        CancellationToken cancellationToken)
    {
        var client = serviceProvider.GetRequiredService<KurrentDBPersistentSubscriptionsClient>();
        var subscriptionExists = true;
        PersistentSubscriptionInfo? info = null;

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                info = await client.GetInfoToAllAsync(
                    this.Settings.ReadModelHydration.SubscriptionName,
                    cancellationToken: cancellationToken);
                break;
            }
            catch (PersistentSubscriptionNotFoundException)
            {
                subscriptionExists = false;
                break;
            }
            catch (Exception exception) when (
                IsTransient(exception) && !cancellationToken.IsCancellationRequested)
            {
                logger.LogWarning(
                    exception,
                    "Transient failure while configuring the read model subscription. Retrying in 5 seconds.");
                await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
            }
        }

        if (!subscriptionExists)
        {
            await client.CreateToAllAsync(
                this.Settings.ReadModelHydration.SubscriptionName,
                this.CreateSubscriptionSettings(),
                cancellationToken: cancellationToken);
        }
        else if (info?.Settings is not null)
        {
            if (info.Settings.CheckPointAfter != TimeSpan.FromMilliseconds(this.Settings.ReadModelHydration.CheckpointAfterMs)
                || info.Settings.CheckPointLowerBound != this.Settings.ReadModelHydration.CheckpointLowerBound
                || info.Settings.CheckPointUpperBound != this.Settings.ReadModelHydration.CheckpointUpperBound)
            {
                await client.UpdateToAllAsync(
                    this.Settings.ReadModelHydration.SubscriptionName,
                    this.CreateSubscriptionSettings(),
                    cancellationToken: cancellationToken);
            }
        }
    }

    private PersistentSubscriptionSettings CreateSubscriptionSettings()
    {
        var subscriptionSettings = this.Settings.ReadModelHydration;
        return new PersistentSubscriptionSettings(
                            resolveLinkTos: true,
                            startFrom: Position.Start,
                            extraStatistics: false,
                            messageTimeout: TimeSpan.FromSeconds(30),
                            maxRetryCount: 10,
                            liveBufferSize: 500,
                            readBatchSize: 20,
                            historyBufferSize: 500,
                            checkPointAfter: TimeSpan.FromMilliseconds(subscriptionSettings.CheckpointAfterMs),
                            checkPointLowerBound: subscriptionSettings.CheckpointLowerBound,
                            checkPointUpperBound: subscriptionSettings.CheckpointUpperBound);
    }

    private static bool IsTransient(Exception exception)
    {
        return exception is NotLeaderException
            || exception is RpcException
            {
                StatusCode: Grpc.Core.StatusCode.Unavailable or Grpc.Core.StatusCode.DeadlineExceeded
            };
    }

    private async Task<IReadModelHydrator> StartHydration(IServiceProvider serviceProvider, CancellationToken stoppingToken)
    {
        var hydrator = serviceProvider.GetRequiredService<IReadModelHydrator>();
        await hydrator.Subscribe(this.Settings.ReadModelHydration.SubscriptionName, stoppingToken);
        return hydrator;
    }
}
