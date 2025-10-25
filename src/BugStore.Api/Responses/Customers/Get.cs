using BugStore.Api.Models;

namespace BugStore.Api.Responses.Customers;

public class Get : PagedResponse<List<Customer>>
{
  public Get(List<Customer>? data, int totalCount, int currentPage, int pageSize) : base(data, totalCount, currentPage, pageSize)
  {

  }

  public Get(List<Customer>? data, int statusCode = 200, string? message = null) : base(data, statusCode, message)
  {

  }
}