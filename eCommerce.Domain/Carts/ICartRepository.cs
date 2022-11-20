using System.Threading.Tasks;
using System.Threading;
using System;

namespace eCommerce.Domain.Carts;

public interface ICartRepository
{
    Task<Cart> GetCartByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
    Task AddCartAsync(Cart cart, CancellationToken cancellationToken = default);
    Task SaveAsync(Cart cart, CancellationToken cancellationToken = default);
}
