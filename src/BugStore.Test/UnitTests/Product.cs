namespace BugStore.Test.UnitTests;

public class Product
{
    [Fact]
    public async Task Should_Create_Product()
    {
        var request = new Api.Requests.Products.Create
        {
            Title = "Test Product",
            Slug = "test-product",
            Price = 9.99m,
            Description = "This is a test product."
        };

        var handler = new FakeProductsHandler();
        var createdProduct = await handler.CreateProductAsync(request, TestContext.Current.CancellationToken);
        Assert.True(createdProduct.Data?.Title == request.Title);
    }

    [Fact]
    public async Task Should_Update_Product()
    {
        var handler = new FakeProductsHandler();
        var createRequest = new Api.Requests.Products.Create
        {
            Title = "Initial Product",
            Slug = "initial-product",
            Price = 19.99m,
            Description = "Initial description."
        };
        var createdProduct = await handler.CreateProductAsync(createRequest, TestContext.Current.CancellationToken);
        var updateRequest = new Api.Requests.Products.Update
        {
            Id = createdProduct.Data!.Id,
            Title = "Updated Product",
            Slug = "updated-product",
            Price = 29.99m,
            Description = "Updated description."
        };
        var updatedProduct = await handler.UpdateProductAsync(updateRequest, TestContext.Current.CancellationToken);
        Assert.True(updatedProduct.Data?.Title == updateRequest.Title);
    }

    [Fact]
    public async Task Should_Delete_Product()
    {
        var handler = new FakeProductsHandler();
        var createRequest = new Api.Requests.Products.Create
        {
            Title = "Product to Delete",
            Slug = "product-to-delete",
            Price = 14.99m,
            Description = "This product will be deleted."
        };
        var createdProduct = await handler.CreateProductAsync(createRequest, TestContext.Current.CancellationToken);
        var deleteRequest = new Api.Requests.Products.Delete
        {
            Id = createdProduct.Data!.Id
        };
        var deleteResult = await handler.DeleteProductAsync(deleteRequest, TestContext.Current.CancellationToken);
        Assert.True(deleteResult.IsSuccess);
    }

    [Fact]
    public async Task Should_Get_Product_By_Id()
    {
        var handler = new FakeProductsHandler();
        var createRequest = new Api.Requests.Products.Create
        {
            Title = "Product to Retrieve",
            Slug = "product-to-retrieve",
            Price = 24.99m,
            Description = "This product will be retrieved by ID."
        };
        var createdProduct = await handler.CreateProductAsync(createRequest, TestContext.Current.CancellationToken);
        var getByIdRequest = new Api.Requests.Products.GetById
        {
            Id = createdProduct.Data!.Id
        };
        var retrievedProduct = await handler.GetProductByIdAsync(getByIdRequest, TestContext.Current.CancellationToken);
        Assert.True(retrievedProduct.Data?.Id == createdProduct.Data.Id);
    }

    [Fact]
    public async Task Should_Get_Products_List()
    {
        var handler = new FakeProductsHandler();
        var createRequest = new Api.Requests.Products.Create
        {
            Title = $"Product 1",
            Slug = $"product-1",
            Price = 10.00m,
            Description = $"Description for product 1."
        };

        await handler.CreateProductAsync(createRequest, TestContext.Current.CancellationToken);
        var getRequest = new Api.Requests.Products.Get
        {
            PageNumber = 1,
            PageSize = 10
        };

        var productsList = await handler.GetProductsAsync(getRequest, TestContext.Current.CancellationToken);
        Assert.True(productsList.Data?.Count > 0);
    }
}
