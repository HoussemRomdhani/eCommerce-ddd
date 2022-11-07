using eCommerce.Domain.Carts;
using eCommerce.Domain.Countries;
using eCommerce.Domain.Customers;
using eCommerce.Domain.SharedKernel.Repositories;
using eCommerce.Domain.SharedKernel.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure;

public class CartReadRepository : IReadRepositoryBase<Cart>
{
    private List<Cart> _store = new()
    {
         Cart.Create(Customer.Create(Guid.Parse("5a2ab9b2-d60d-4556-83e2-3c53cbea0e3c"), 
                                     "houssem",
                                     "romdhani",
                                     "houssem.romdhani@hro.com", 
                                     Country.Create("Tunisia")))
    };

    public async Task<Cart> FirstOrDefaultAsync(ISpecification<Cart> specification, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_store.FirstOrDefault(specification.IsSatisfiedBy));
    }

    public async Task<Cart> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(_store.FirstOrDefault(cart => cart.Id == id));
    }
}
