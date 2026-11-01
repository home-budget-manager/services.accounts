namespace HomeBudgetManager.Services.Accounts.Application;

using HomeBudgetManager.Services.Accounts.Domain;

public interface ICommandHandler
{
    Task<OneOf<CommandResults.Success, CommandResults.Failure>> Handle(
        ICommand command,
        CancellationToken cancellationToken);
}
