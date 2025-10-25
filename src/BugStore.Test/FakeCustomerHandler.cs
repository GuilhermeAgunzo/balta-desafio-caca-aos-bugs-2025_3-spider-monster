using BugStore.Api.Abstractions.Handlers.Customers;
using BugStore.Api.Models;

namespace BugStore.Test;

internal class FakeCustomerHandler : IHandler
{
    public List<Customer> Customers { get; set; } = [];

    public async Task<Api.Responses.Customers.Create> CreateCustomerAsync(Api.Requests.Customers.Create request, CancellationToken cancellationToken = default)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            BirthDate = request.BirthDate
        };

        Customers.Add(customer);

        return new Api.Responses.Customers.Create(data: customer);

    }

    public async Task<Api.Responses.Customers.Delete> DeleteCustomerAsync(Api.Requests.Customers.Delete request, CancellationToken cancellationToken = default)
    {
        var customer = Customers.FirstOrDefault(c => c.Id == request.Id);
        if (customer is null)
        {
            return new Api.Responses.Customers.Delete(data: null, statusCode: 404, message: "Customer not found.");
        }
        Customers.Remove(customer);


        return new Api.Responses.Customers.Delete(data: null, statusCode: 200, message: "Customer deleted successfully.");
    }

    public async Task<Api.Responses.Customers.GetById> GetCustomerByIdAsync(Api.Requests.Customers.GetById request, CancellationToken cancellationToken = default)
    {
        var customer = Customers.FirstOrDefault(c => c.Id == request.Id);

        if (customer is null)
        {
            return new Api.Responses.Customers.GetById(data: null, statusCode: 404, message: "Customer not found.");
        }

        return new Api.Responses.Customers.GetById(data: customer);
    }

    public async Task<Api.Responses.Customers.Get> GetCustomersAsync(Api.Requests.Customers.Get request, CancellationToken cancellationToken = default)
    {
        var customers = Customers
          .Take(request.PageSize)
          .Skip((request.PageNumber - 1) * request.PageSize)
          .ToList();

        var total = Customers.Count();

        return new Api.Responses.Customers.Get(
          data: customers,
          totalCount: total,
          currentPage: request.PageNumber,
          pageSize: request.PageSize);
    }

    public async Task<Api.Responses.Customers.Update> UpdateCustomerAsync(Api.Requests.Customers.Update request, CancellationToken cancellationToken = default)
    {
        var customer = Customers.FirstOrDefault(c => c.Id == request.Id);

        if (customer is null)
        {
            return new Api.Responses.Customers.Update(data: null, statusCode: 404, message: "Customer not found.");
        }

        customer.Name = request.Name;
        customer.Email = request.Email;
        customer.Phone = request.Phone ?? "";
        customer.BirthDate = request.BirthDate;

        return new Api.Responses.Customers.Update(data: customer);
    }
}
