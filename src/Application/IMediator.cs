namespace HomeBudgetManager.Services.Accounts.Application;

using HomeBudgetManager.Services.Accounts.Domain;

public interface IMediator
{
    Task<OneOf<CommandResults.Success, CommandResults.Failure>> Execute<TCommand>(TCommand command)
        where TCommand : class, ICommand;

    Task<TResult> Fetch<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
        where TResult : IResult?;
}
