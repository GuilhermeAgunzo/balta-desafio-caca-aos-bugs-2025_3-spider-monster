namespace BugStore.Api.Requests.Customers;

public class GetById : Request
{
  public Guid Id { get; set; }
}