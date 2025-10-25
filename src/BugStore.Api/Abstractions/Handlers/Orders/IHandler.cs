namespace BugStore.Api.Abstractions.Handlers.Orders;

public interface IHandler
{
    Task<Responses.Orders.GetById> GetOrderByIdAsync(Requests.Orders.GetById request, CancellationToken cancellationToken = default);
    Task<Responses.Orders.Create> CreateOrderAsync(Requests.Orders.Create request, CancellationToken cancellationToken = default);
}
