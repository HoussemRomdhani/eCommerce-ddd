using eCommerce.Domain.SharedKernel;
using eCommerce.Domain.SharedKernel.Repositories;
using System.Collections.Generic;

namespace eCommerce.Domain.Customers.Repositories;

public interface ICustomerRepository : IRepositoryBase<Customer>
{
    IEnumerable<CustomerPurchaseHistoryReadModel> GetCustomersPurchaseHistory();
}
