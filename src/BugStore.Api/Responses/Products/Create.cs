using BugStore.Api.Models;

namespace BugStore.Api.Responses.Products;

public class Create : Response<Product>
{
  public Create(Product? data, int statusCode = 201, string message = "Product created successfully.") : base(data, statusCode, message)
  {
  }

}