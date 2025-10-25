using BugStore.Api.Models;

namespace BugStore.Api.Responses.Customers;

public class Delete : Response<Customer>
{
  public Delete(Customer? data, int statusCode = 200, string message = "Request processed successfully.") : base(data, statusCode, message)
  {
  }

}