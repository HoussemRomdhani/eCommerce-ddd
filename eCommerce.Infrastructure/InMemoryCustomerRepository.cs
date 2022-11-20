using eCommerce.Domain.Countries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eCommerce.Domain.Customers;
using System.Linq;

namespace eCommerce.Infrastructure
{
    public class InMemoryCustomerRepository : ICustomerRepository
    {
        private List<Customer> _store = new()
        {
            Customer.Create("houssem",
                            "romdhani",
                            "houssem.romdhani@hro.com",
                             Country.Create(Guid.Parse("4f14a0df-b397-4ca3-83d5-d3b759a74de4"), "FRA" ,"France"))
        };

        public Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_store.FirstOrDefault(x => x.Id == id));
        }

        public Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            _store.Add(customer);
            return Task.CompletedTask;
        }

        public IEnumerable<CustomerPurchaseHistoryReadModel> GetCustomersPurchaseHistory()
        {
            return Enumerable.Empty<CustomerPurchaseHistoryReadModel>();
        }

        public Task<Customer> GetCustomerByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_store.FirstOrDefault(x => x.Email == email));
        }

        public Task DeleteCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            var customerExists = _store.FirstOrDefault(x => x.Id == customer.Id);
            if (customerExists != null)
            {
                _store.Remove(customerExists);
            }

            return Task.CompletedTask;
        }

        public void UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
        {
            var customerExists = _store.FirstOrDefault(x => x.Id == customer.Id);
            if (customerExists != null)
            {
                _store.Remove(customerExists);
                _store.Add(customer);
            }
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IReadOnlyList<Customer>)_store.AsReadOnly()) ;
        }

        
    }
}
