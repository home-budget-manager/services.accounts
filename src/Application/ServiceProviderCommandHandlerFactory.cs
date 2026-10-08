namespace HomeBudgetManager.Services.Accounts.Application;

using HomeBudgetManager.Services.Accounts.Domain;

using Microsoft.Extensions.DependencyInjection;

public sealed class ServiceProviderCommandHandlerFactory(IServiceProvider serviceProvider) : ICommandHandlerFactory
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public static string GetCommandHandlerKey<TCommand>()
        where TCommand : class, ICommand
    {
        return typeof(TCommand).AssemblyQualifiedName ?? throw new InvalidOperationException($"Cannot get assembly qualified name for type '{typeof(TCommand).FullName}'");
    }

    public ICommandHandler? Create<TCommand>()
        where TCommand : class, ICommand
    {
        return serviceProvider.GetKeyedService<ICommandHandler>(GetCommandHandlerKey<TCommand>());
    }
}
