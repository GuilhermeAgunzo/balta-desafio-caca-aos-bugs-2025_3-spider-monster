namespace BugStore.Api.Requests.Products;

public class Delete : Request
{
  public required Guid Id { get; set; }
}