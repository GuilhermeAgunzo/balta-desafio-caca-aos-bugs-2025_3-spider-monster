using BugStore.Api.Abstractions.Handlers.Products;
using BugStore.Api.Models;

namespace BugStore.Test;

public class FakeProductsHandler : IHandler
{
    private List<Product> Products { get; set; } = [];

    public async Task<Api.Responses.Products.Create> CreateProductAsync(Api.Requests.Products.Create request, CancellationToken cancellationToken = default)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description ?? "",
            Slug = request.Slug,
            Price = request.Price
        };

        Products.Add(product);

        return new Api.Responses.Products.Create(product);
    }

    public async Task<Api.Responses.Products.Delete> DeleteProductAsync(Api.Requests.Products.Delete request, CancellationToken cancellationToken = default)
    {
        var product = Products.FirstOrDefault(p => p.Id == request.Id);
        if (product is null)
        {
            return new Api.Responses.Products.Delete(null, 404, "Product not found.");
        }

        Products.Remove(product);

        return new Api.Responses.Products.Delete(data: null);
    }

    public async Task<Api.Responses.Products.GetById> GetProductByIdAsync(Api.Requests.Products.GetById request, CancellationToken cancellationToken = default)
    {
        var product = Products.FirstOrDefault(p => p.Id == request.Id);

        if (product is null)
        {
            return new Api.Responses.Products.GetById(null, 404, "Product not found.");
        }

        return new Api.Responses.Products.GetById(product);
    }

    public async Task<Api.Responses.Products.Get> GetProductsAsync(Api.Requests.Products.Get request, CancellationToken cancellationToken = default)
    {
        var products = Products
          .Take(request.PageSize)
          .Skip((request.PageNumber - 1) * request.PageSize)
          .ToList();

        var total = Products.Count();

        return new Api.Responses.Products.Get(
          data: products,
          totalCount: total,
          currentPage: request.PageNumber,
          pageSize: request.PageSize);
    }

    public async Task<Api.Responses.Products.Update> UpdateProductAsync(Api.Requests.Products.Update request, CancellationToken cancellationToken = default)
    {
        var product = Products.FirstOrDefault(p => p.Id == request.Id);

        if (product is null)
        {
            return new Api.Responses.Products.Update(null, 404, "Product not found.");
        }

        product.Title = request.Title;
        product.Description = request.Description ?? "";
        product.Slug = request.Slug;
        product.Price = request.Price;

        return new Api.Responses.Products.Update(product);
    }
}
