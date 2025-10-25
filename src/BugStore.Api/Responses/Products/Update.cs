using BugStore.Api.Models;
using BugStore.Api.Responses;

namespace BugStore.Api.Responses.Products;

public class Update : Response<Product>
{
  public Update(Product? data, int statusCode = 200, string message = "Request processed successfully.") : base(data, statusCode, message)
  {
  }
}