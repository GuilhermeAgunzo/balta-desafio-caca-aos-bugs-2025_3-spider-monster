using BugStore.Api.Models;
using BugStore.Api.Responses;

namespace BugStore.Api.Responses.Customers;

public class GetById : Response<Customer>
{
  public GetById(Customer? data, int statusCode = 200, string? message = null) : base(data, statusCode, message)
  {

  }
}