namespace BugStore.Api.Abstractions.Handlers.Customers;

public interface IHandler
{
    Task<Responses.Customers.Get> GetCustomersAsync(Requests.Customers.Get request, CancellationToken cancellationToken = default);
    Task<Responses.Customers.GetById> GetCustomerByIdAsync(Requests.Customers.GetById request, CancellationToken cancellationToken = default);
    Task<Responses.Customers.Create> CreateCustomerAsync(Requests.Customers.Create request, CancellationToken cancellationToken = default);
    Task<Responses.Customers.Update> UpdateCustomerAsync(Requests.Customers.Update request, CancellationToken cancellationToken = default);
    Task<Responses.Customers.Delete> DeleteCustomerAsync(Requests.Customers.Delete request, CancellationToken cancellationToken = default);
}
