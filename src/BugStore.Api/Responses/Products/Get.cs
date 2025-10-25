using BugStore.Api.Models;
using BugStore.Api.Responses;

namespace BugStore.Api.Responses.Products;

public class Get : PagedResponse<List<Product>>
{
  public Get(List<Product>? data, int totalCount, int currentPage, int pageSize) : base(data, totalCount, currentPage, pageSize)
  {

  }

  public Get(List<Product>? data, int statusCode = 200, string? message = null) : base(data, statusCode, message)
  {

  }
}