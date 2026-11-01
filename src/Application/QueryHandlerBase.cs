namespace HomeBudgetManager.Services.Accounts.Application;

using HomeBudgetManager.Services.Accounts.Domain;

public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler<TResult>
    where TQuery : class, IQuery<TResult>
    where TResult : IResult?
{
    public async Task<TResult> Handle(IQuery<TResult> query, CancellationToken cancellationToken)
    {
        if (query is not TQuery typedQuery)
        {
            throw new ArgumentException($"Invalid query type. Expected '{typeof(TQuery).FullName}', but received '{query.GetType().FullName}'.");
        }

        return await this.Handle(typedQuery, cancellationToken);
    }

    protected abstract Task<TResult> Handle(TQuery query, CancellationToken cancellationToken);
}
