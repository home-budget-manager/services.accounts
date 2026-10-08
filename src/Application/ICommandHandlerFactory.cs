namespace HomeBudgetManager.Services.Accounts.Application;

using HomeBudgetManager.Services.Accounts.Domain;

public interface ICommandHandlerFactory
{
    ICommandHandler? Create<TCommand>()
        where TCommand : class, ICommand;
}
