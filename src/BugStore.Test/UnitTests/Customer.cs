namespace BugStore.Test.UnitTests;

public class Customer
{
    [Fact]
    public async Task Unit_Should_Create_Customer()
    {

        var customer = new BugStore.Api.Requests.Customers.Create
        {
            Name = "John Doe",
            Email = "john@doe.com",
            Phone = "123456",
            BirthDate = new DateTime(1990, 1, 1)
        };

        var handler = new FakeCustomerHandler();

        var result = await handler.CreateCustomerAsync(customer, TestContext.Current.CancellationToken);

        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Unit_Should_Get_Customer_By_Id()
    {
        var handler = new FakeCustomerHandler();
        var customer = new BugStore.Api.Requests.Customers.Create
        {

            Name = "John Doe",
            Email = "",
            BirthDate = new DateTime(1990, 1, 1),
            Phone = "123456"
        };

        var createResult = await handler.CreateCustomerAsync(customer, TestContext.Current.CancellationToken);
        var getByIdRequest = new BugStore.Api.Requests.Customers.GetById
        {
            Id = createResult.Data!.Id
        };
        var getByIdResult = await handler.GetCustomerByIdAsync(getByIdRequest, TestContext.Current.CancellationToken);
        Assert.True(getByIdResult.Data?.Id == getByIdRequest.Id);
    }

    [Fact]
    public async Task Unit_Should_Delete_Customer()
    {
        var handler = new FakeCustomerHandler();
        var customer = new BugStore.Api.Requests.Customers.Create
        {
            Name = "John Doe",
            Email = "",
            BirthDate = new DateTime(1990, 1, 1),
            Phone = "123456"
        };
        var createResult = await handler.CreateCustomerAsync(customer, TestContext.Current.CancellationToken);
        var deleteRequest = new BugStore.Api.Requests.Customers.Delete
        {
            Id = createResult.Data!.Id
        };
        var deleteResult = await handler.DeleteCustomerAsync(deleteRequest, TestContext.Current.CancellationToken);
        Assert.True(deleteResult.IsSuccess);
    }

    [Fact]
    public async Task Unit_Should_Get_Customers()
    {
        var handler = new FakeCustomerHandler();

        var customer = new BugStore.Api.Requests.Customers.Create
        {
            Name = $"John Doe",
            Email = "",
            BirthDate = new DateTime(1990, 1, 1),
            Phone = "123456"
        };

        await handler.CreateCustomerAsync(customer, TestContext.Current.CancellationToken);
        var getRequest = new BugStore.Api.Requests.Customers.Get
        {
            PageNumber = 1,
            PageSize = 10
        };

        var getResult = await handler.GetCustomersAsync(getRequest, TestContext.Current.CancellationToken);
        Assert.True(getResult.Data?.Count > 0);
    }

    [Fact]
    public async Task Unit_Should_Update_Customer()
    {
        var handler = new FakeCustomerHandler();
        var customer = new BugStore.Api.Requests.Customers.Create
        {
            Name = "John Doe",
            Email = "",
            BirthDate = new DateTime(1990, 1, 1),
            Phone = "123456"
        };
        var createResult = await handler.CreateCustomerAsync(customer, TestContext.Current.CancellationToken);
        var updateRequest = new BugStore.Api.Requests.Customers.Update
        {
            Id = createResult.Data!.Id,
            Name = "Jane Doe",
            Email = "jane@doe.com",
            BirthDate = new DateTime(1992, 2, 2),
            Phone = "654321"
        };
        var updateResult = await handler.UpdateCustomerAsync(updateRequest, TestContext.Current.CancellationToken);
        Assert.True(updateResult.Data?.Name == updateRequest.Name);
    }
}

