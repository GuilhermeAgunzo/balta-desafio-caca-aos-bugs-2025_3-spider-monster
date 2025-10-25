using BugStore.Api.Models;

namespace BugStore.Api.Requests.Orders;

public class Create : Request
{
  public Guid CustomerId { get; set; }
  public required Customer Customer { get; set; }
  public List<OrderLine> Lines { get; set; } = [];

}