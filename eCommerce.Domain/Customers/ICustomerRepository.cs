using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace eCommerce.Domain.Customers;

public interface ICustomerRepository
{
    Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Customer> GetCustomerByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
    IEnumerable<CustomerPurchaseHistoryReadModel> GetCustomersPurchaseHistory();
    Task DeleteCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Customer>> GetAllAsync(CancellationToken cancellationToken = default);
    void UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken = default);
}
