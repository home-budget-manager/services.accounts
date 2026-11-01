namespace HomeBudgetManager.Services.Accounts.Application;

using HomeBudgetManager.Services.Accounts.Domain;

public abstract class CommandHandlerBase<TCommand> : ICommandHandler
    where TCommand : ICommand
{
    public async Task<OneOf<CommandResults.Success, CommandResults.Failure>> Handle(
        ICommand command,
        CancellationToken cancellationToken)
    {
        if (command is not TCommand typedCommand)
        {
            return new CommandResults.Failure($"Invalid command type. Expected '{typeof(TCommand).FullName}', but received '{command.GetType().FullName}'.");
        }

        return await this.Handle(typedCommand, cancellationToken);
    }

    protected abstract Task<OneOf<CommandResults.Success, CommandResults.Failure>> Handle(
        TCommand command,
        CancellationToken cancellationToken);
}
