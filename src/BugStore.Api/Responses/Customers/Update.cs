using BugStore.Api.Models;
using BugStore.Api.Responses;

namespace BugStore.Api.Responses.Customers;

public class Update : Response<Customer>
{
  public Update(Customer? data, int statusCode = 200, string message = "Request processed successfully.") : base(data, statusCode, message)
  {
  }

}