using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace eCommerce.Domain.Products;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddProductAsync(Product product, CancellationToken cancellationToken = default);
    Task UpdateProductAsync(Product product, CancellationToken cancellationToken = default);
    Task<ProductCode> GetProductCodeByNameAsync(string name, CancellationToken cancellationToken = default);
}
