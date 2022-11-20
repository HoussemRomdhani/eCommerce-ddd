using eCommerce.Domain.Carts;
using eCommerce.Domain.Countries;
using eCommerce.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure;

public class InMemoryCartRepository : ICartRepository
{
    private List<Cart> _store = new()
    {
         Cart.Create(Customer.Create("houssem",
                                     "romdhani",
                                     "houssem.romdhani@hro.com", 
                                     Country.Create("FRA", "France")))
    };

    public Task AddCartAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        _store.Add(cart);
        return Task.CompletedTask;
    }

    public Task<Cart> GetCartByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.FirstOrDefault(x => x.CustomerId == customerId));
    }

    public Task SaveAsync(Cart cart, CancellationToken cancellationToken = default)
    {
        var cartExists = _store.FirstOrDefault(c => c.Id == cart.Id);
        if (cartExists != null)
        {
            _store.Remove(cartExists);
            _store.Add(cartExists);
        }

        return Task.CompletedTask;
    }
}
