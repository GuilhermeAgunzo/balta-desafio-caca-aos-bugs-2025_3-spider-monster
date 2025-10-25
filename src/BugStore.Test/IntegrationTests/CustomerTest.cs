using System.Net.Http.Json;
using System.Text.Json;

namespace BugStore.Test.IntegrationTests;

public class CustomerTest : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "v1/customers";
    private readonly HttpClient _httpClient;

    public CustomerTest(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task Should_Create_Customer()
    {
        var request = new Api.Requests.Customers.Create
        {
            Name = "John Doe",
            Phone = "5511111111111",
            BirthDate = new DateTime(1990, 1, 1),
            Email = "john@doe.com"
        };

        var response = await _httpClient.PostAsJsonAsync(METHOD, request, TestContext.Current.CancellationToken);
        var result = await response.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);

        var json = await JsonDocument.ParseAsync(result, cancellationToken: TestContext.Current.CancellationToken);

        var name = json.RootElement.GetProperty("data").GetProperty("name").GetString();

        Assert.True(request.Name == name);
    }

    [Fact]
    public async Task Should_Update_Customer()
    {
        var createRequest = new Api.Requests.Customers.Create
        {
            Name = "Jane Doe",
            Phone = "5522222222222",
            BirthDate = new DateTime(1992, 2, 2),
            Email = "jane@doe.com"
        };

        var createResponse = await _httpClient.PostAsJsonAsync(METHOD, createRequest, TestContext.Current.CancellationToken);
        var createResult = await createResponse.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);
        var createJson = await JsonDocument.ParseAsync(createResult, cancellationToken: TestContext.Current.CancellationToken);
        var createdCustomerId = createJson.RootElement.GetProperty("data").GetProperty("id").GetGuid();

        var updateRequest = new Api.Requests.Customers.Update
        {
            Name = "Jane Smith",
            Phone = "5533333333333",
            BirthDate = new DateTime(1992, 2, 2),
            Email = "jane@smith.com"
        };

        var response = await _httpClient.PutAsJsonAsync($"{METHOD}/{createdCustomerId}", updateRequest, TestContext.Current.CancellationToken);

        var result = await response.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);
        var json = await JsonDocument.ParseAsync(result, cancellationToken: TestContext.Current.CancellationToken);
        var name = json.RootElement.GetProperty("data").GetProperty("name").GetString();

        Assert.True(updateRequest.Name == name);
    }

    [Fact]
    public async Task Should_Get_Customer_By_Id()
    {
        var createRequest = new Api.Requests.Customers.Create
        {
            Name = "Alice Doe",
            Phone = "5544444444444",
            BirthDate = new DateTime(1994, 4, 4),
            Email = "alice@doe.com"
        };

        var createResponse = await _httpClient.PostAsJsonAsync(METHOD, createRequest, TestContext.Current.CancellationToken);
        var createResult = await createResponse.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);
        var createJson = await JsonDocument.ParseAsync(createResult, cancellationToken: TestContext.Current.CancellationToken);
        var createdCustomerId = createJson.RootElement.GetProperty("data").GetProperty("id").GetGuid();

        var response = await _httpClient.GetAsync($"{METHOD}/{createdCustomerId}", TestContext.Current.CancellationToken);
        var result = await response.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);
        var json = await JsonDocument.ParseAsync(result, cancellationToken: TestContext.Current.CancellationToken);
        var name = json.RootElement.GetProperty("data").GetProperty("name").GetString();
        Assert.True(createRequest.Name == name);
    }

    [Fact]
    public async Task Should_Get_Customers_List()
    {
        var response = await _httpClient.GetAsync(METHOD, TestContext.Current.CancellationToken);
        var result = await response.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);
        var json = await JsonDocument.ParseAsync(result, cancellationToken: TestContext.Current.CancellationToken);
        var customers = json.RootElement.GetProperty("data").EnumerateArray().ToList();
        Assert.True(customers.Count > 0);
    }

    [Fact]
    public async Task Should_Delete_Customer()
    {
        var createRequest = new Api.Requests.Customers.Create
        {
            Name = "Bob Doe",
            Phone = "5555555555555",
            BirthDate = new DateTime(1995, 5, 5),
            Email = "bob@doe.com"
        };

        var createResponse = await _httpClient.PostAsJsonAsync(METHOD, createRequest, TestContext.Current.CancellationToken);
        var createResult = await createResponse.Content.ReadAsStreamAsync(TestContext.Current.CancellationToken);
        var createJson = await JsonDocument.ParseAsync(createResult, cancellationToken: TestContext.Current.CancellationToken);
        var createdCustomerId = createJson.RootElement.GetProperty("data").GetProperty("id").GetGuid();

        var response = await _httpClient.DeleteAsync($"{METHOD}/{createdCustomerId}", TestContext.Current.CancellationToken);
        response.EnsureSuccessStatusCode();

        var getResponse = await _httpClient.GetAsync($"{METHOD}/{createdCustomerId}", TestContext.Current.CancellationToken);
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, getResponse.StatusCode);
    }
}
