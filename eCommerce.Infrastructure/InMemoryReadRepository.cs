using eCommerce.Domain.SharedKernel;
using eCommerce.Domain.SharedKernel.Repositories;
using eCommerce.Domain.SharedKernel.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure
{
    public class InMemoryReadRepository<T> : IReadRepositoryBase<T> where T: EntityBase
    {
        private List<T> _store = new ();

        public async Task<T> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(_store.FirstOrDefault(specification.IsSatisfiedBy));
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(_store.FirstOrDefault(cart => cart.Id == id));
        }
    }
}
