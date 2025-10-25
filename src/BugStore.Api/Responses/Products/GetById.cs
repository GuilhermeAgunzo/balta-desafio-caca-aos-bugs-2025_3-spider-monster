using BugStore.Api.Models;
using BugStore.Api.Responses;

namespace BugStore.Api.Responses.Products;

public class GetById : Response<Product>
{
  public GetById(Product? data, int statusCode = 200, string message = "Request processed successfully.") : base(data, statusCode, message)
  {
  }
}