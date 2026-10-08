namespace HomeBudgetManager.Services.Accounts.Application;

using HomeBudgetManager.Services.Accounts.Domain;

public sealed class Mediator(
    ICommandHandlerFactory commandHandlerFactory,
    IQueryHandlerFactory queryHandlerFactory) : IMediator
{
    private readonly ICommandHandlerFactory commandHandlerFactory = commandHandlerFactory;

    private readonly IQueryHandlerFactory queryHandlerFactory = queryHandlerFactory;

    public async Task<OneOf<CommandResults.Success, CommandResults.Failure>> Execute<TCommand>(TCommand command)
        where TCommand : class, ICommand
    {
        var commandHandler = commandHandlerFactory.Create<TCommand>();
        if (commandHandler is null)
        {
            return new CommandResults.Failure($"No handler found for command of type '{typeof(TCommand).FullName}'");
        }

        return await commandHandler.Handle(command, CancellationToken.None);
    }

    public async Task<TResult> Fetch<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        where TResult : IResult?
    {
        var queryHandler = queryHandlerFactory.Create<TResult>();
        if (queryHandler is null)
        {
            throw new InvalidOperationException($"No handler found for query of type '{typeof(TResult).FullName}'");
        }

        return await queryHandler.Handle(query, cancellationToken);
    }
}
