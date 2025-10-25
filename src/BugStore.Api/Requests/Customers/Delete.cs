namespace BugStore.Api.Requests.Customers;

public class Delete : Request
{
  public Guid Id { get; set; }
}