using System.Net.Http.Json;
using System.Text.Json;

namespace BugStore.Test;

public class ProductTest : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "v1/products";
    private readonly HttpClient _httpClient;

    public ProductTest(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task Should_Create_Product()
    {
        var request = new BugStore.Api.Requests.Products.Create
        {
            Title = "Sample Product",
            Description = "This is a sample product.",
            Price = 19.99M,
            Slug = "sample-product"
        };

        var response = await _httpClient.PostAsJsonAsync(METHOD, request, TestContext.Current.CancellationToken);
        var result = await response.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);

        var json = await JsonDocument.ParseAsync(result, cancellationToken: TestContext.Current.CancellationToken);

        var title = json.RootElement.GetProperty("data").GetProperty("title").GetString();

        Assert.True(request.Title == title);
    }

    [Fact]
    public async Task Should_Update_Product()
    {
        var createRequest = new BugStore.Api.Requests.Products.Create
        {
            Slug = "old-product",
            Title = "Old Product",
            Description = "This is the old product.",
            Price = 9.99M
        };

        var createResponse = await _httpClient.PostAsJsonAsync(METHOD, createRequest, TestContext.Current.CancellationToken);
        var createResult = await createResponse.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);
        var createJson = await JsonDocument.ParseAsync(createResult, cancellationToken: TestContext.Current.CancellationToken);
        var createdProductId = createJson.RootElement.GetProperty("data").GetProperty("id").GetGuid();

        var updateRequest = new BugStore.Api.Requests.Products.Update
        {
            Id = createdProductId,
            Slug = "updated-product",
            Title = "Updated Product",
            Description = "This is the updated product.",
            Price = 14.99M
        };

        var response = await _httpClient.PutAsJsonAsync($"{METHOD}/{createdProductId}", updateRequest, TestContext.Current.CancellationToken);

        var result = await response.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);
        var json = await JsonDocument.ParseAsync(result, cancellationToken: TestContext.Current.CancellationToken);
        var title = json.RootElement.GetProperty("data").GetProperty("title").GetString();

        Assert.True(updateRequest.Title == title);
    }

    [Fact]
    public async Task Should_Get_Product_By_Id()
    {
        var createRequest = new BugStore.Api.Requests.Products.Create
        {
            Title = "John's Product",
            Description = "This is John's product.",
            Price = 29.99M,
            Slug = "johns-product"
        };

        var createResponse = await _httpClient.PostAsJsonAsync(METHOD, createRequest, TestContext.Current.CancellationToken);
        var createResult = await createResponse.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);
        var createJson = await JsonDocument.ParseAsync(createResult, cancellationToken: TestContext.Current.CancellationToken);
        var createdProductId = createJson.RootElement.GetProperty("data").GetProperty("id").GetGuid();

        var response = await _httpClient.GetAsync($"{METHOD}/{createdProductId}", TestContext.Current.CancellationToken);
        var result = await response.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);
        var json = await JsonDocument.ParseAsync(result, cancellationToken: TestContext.Current.CancellationToken);
        var title = json.RootElement.GetProperty("data").GetProperty("title").GetString();
        Assert.True(createRequest.Title == title);
    }

    [Fact]
    public async Task Should_Get_Products_List()
    {
        var createRequest = new BugStore.Api.Requests.Products.Create
        {
            Title = "John's Product",
            Description = "This is John's product.",
            Price = 29.99M,
            Slug = "johns-product"
        };

        await _httpClient.PostAsJsonAsync(METHOD, createRequest, TestContext.Current.CancellationToken);

        var response = await _httpClient.GetAsync(METHOD, TestContext.Current.CancellationToken);
        var result = await response.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);
        var json = await JsonDocument.ParseAsync(result, cancellationToken: TestContext.Current.CancellationToken);
        var products = json.RootElement.GetProperty("data").EnumerateArray().ToList();
        Assert.True(products.Count > 0);
    }

    [Fact]
    public async Task Should_Delete_Product()
    {
        var createRequest = new BugStore.Api.Requests.Products.Create
        {
            Slug = "temp-product",
            Title = "Temp Product",
            Description = "This is a temporary product.",
            Price = 4.99M
        };

        var createResponse = await _httpClient.PostAsJsonAsync(METHOD, createRequest, TestContext.Current.CancellationToken);
        var createResult = await createResponse.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);
        var createJson = await JsonDocument.ParseAsync(createResult, cancellationToken: TestContext.Current.CancellationToken);
        var createdProductId = createJson.RootElement.GetProperty("data").GetProperty("id").GetGuid();

        var response = await _httpClient.DeleteAsync($"{METHOD}/{createdProductId}", TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var getResponse = await _httpClient.GetAsync($"{METHOD}/{createdProductId}", TestContext.Current.CancellationToken);
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, getResponse.StatusCode);
    }
}
