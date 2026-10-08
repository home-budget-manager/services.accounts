namespace HomeBudgetManager.Services.Accounts.WebApi.UnitTests.ControllersTests.UserAccountsControllerTests;

using HomeBudgetManager.Services.Accounts.Application;
using HomeBudgetManager.Services.Accounts.WebApi.Controllers;

public abstract class TestsBase
{
    private readonly Mock<IMediator> mediatorMock = new();

    private UserAccountsController instance = null!;

    protected UserAccountsController Instance => this.instance;

    protected Mock<IMediator> MediatorMock => this.mediatorMock;

    protected void InstanceIsCreated()
    {
        this.instance = new UserAccountsController(this.mediatorMock.Object);
    }
}
