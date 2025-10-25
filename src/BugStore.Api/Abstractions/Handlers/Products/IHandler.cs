namespace BugStore.Api.Abstractions.Handlers.Products;

public interface IHandler
{
    Task<Responses.Products.Get> GetProductsAsync(Requests.Products.Get request, CancellationToken cancellationToken = default);
    Task<Responses.Products.GetById> GetProductByIdAsync(Requests.Products.GetById request, CancellationToken cancellationToken = default);
    Task<Responses.Products.Create> CreateProductAsync(Requests.Products.Create request, CancellationToken cancellationToken = default);
    Task<Responses.Products.Update> UpdateProductAsync(Requests.Products.Update request, CancellationToken cancellationToken = default);
    Task<Responses.Products.Delete> DeleteProductAsync(Requests.Products.Delete request, CancellationToken cancellationToken = default);
}
