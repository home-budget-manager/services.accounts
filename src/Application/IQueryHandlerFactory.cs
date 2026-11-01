namespace HomeBudgetManager.Services.Accounts.Application;

using HomeBudgetManager.Services.Accounts.Domain;

public interface IQueryHandlerFactory
{
    IQueryHandler<TResult>? Create<TResult>()
        where TResult : IResult?;
}
