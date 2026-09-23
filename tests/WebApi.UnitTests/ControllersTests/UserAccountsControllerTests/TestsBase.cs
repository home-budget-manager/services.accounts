namespace HomeBudgetManager.Services.Accounts.WebApi.UnitTests.ControllersTests.UserAccountsControllerTests;

using HomeBudgetManager.Services.Accounts.WebApi.Controllers;

public abstract class TestsBase
{
    private UserAccountsController instance = null!;

    protected UserAccountsController Instance => this.instance;

    protected void InstanceIsCreated()
    {
        this.instance = new UserAccountsController();
    }
}
