namespace HomeBudgetManager.Services.Accounts.Application;

using HomeBudgetManager.Services.Accounts.Domain;

public interface IQueryHandler<TResult>
    where TResult : IResult?
{
    Task<TResult> Handle(IQuery<TResult> query, CancellationToken cancellationToken);
}
