using eCommerce.Domain.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure;

public class InMemoryProductRepository : IProductRepository
{
    private List<Product> _store = new()
        {
            Product.Create("IPhone 10", 10, 1099, ProductCode.Create("APPLE"))
        };

    public Task AddProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        _store.Add(product);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult((IReadOnlyList<Product>)_store.AsReadOnly());
    }

    public Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.FirstOrDefault(x => x.Id == id));
    }

    public Task<ProductCode> GetProductCodeByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.FirstOrDefault(x => x.Code.Name == name)?.Code);
    }

    public Task UpdateProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        var productExists = _store.FirstOrDefault(x => x.Id == product.Id);
       
        if (productExists != null)
        {
            _store.Remove(productExists);
            _store.Add(product);
        }

        return Task.CompletedTask;
    }
}
