using eCommerce.Domain.Countries;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Customers.Repositories;
using eCommerce.Domain.SharedKernel.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private List<Customer> _store = new()
        {
            Customer.Create(Guid.Parse("5a2ab9b2-d60d-4556-83e2-3c53cbea0e3c"),
                            "houssem",
                            "romdhani",
                            "houssem.romdhani@hro.com",
                            Country.Create("TUN"))
        };

        public Task AddAsync(Customer entity, CancellationToken cancellationToken = default)
        {
            _store.Add(entity);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Customer entity, CancellationToken cancellationToken = default)
        {
            var itemToRemove = _store.SingleOrDefault(r => r.Id == entity.Id);

            if (itemToRemove != null)
                _store.Remove(itemToRemove);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Customer entity, CancellationToken cancellationToken = default)
        {
            var indexOf = _store.IndexOf(_store.Find(p => p.Id == entity.Id));
            if (indexOf >= 0)
            {
                _store[indexOf] = entity;
            }

            return Task.CompletedTask;
        }

        public Task<Customer> FirstOrDefaultAsync(ISpecification<Customer> specification, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_store.FirstOrDefault(specification.IsSatisfiedBy));
        }

        public Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_store.FirstOrDefault(x => x.Id == id));
        }

        public IEnumerable<CustomerPurchaseHistoryReadModel> GetCustomersPurchaseHistory()
        {
            return new List<CustomerPurchaseHistoryReadModel>();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(1);
        }
    }
}
