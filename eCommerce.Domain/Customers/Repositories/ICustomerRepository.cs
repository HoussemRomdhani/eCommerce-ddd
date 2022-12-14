using eCommerce.Domain.Common;
using System.Collections.Generic;

namespace eCommerce.Domain.Customers.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<CustomerPurchaseHistoryReadModel> GetCustomersPurchaseHistory();
    }
}
