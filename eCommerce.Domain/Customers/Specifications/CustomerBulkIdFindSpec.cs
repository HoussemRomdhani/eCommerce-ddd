using eCommerce.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace eCommerce.Domain.Customers.Specifications
{
    public class CustomerBulkIdFindSpec : SpecificationBase<Customer>
    {
        private readonly IEnumerable<Guid> customerIds;
        public CustomerBulkIdFindSpec(IEnumerable<Guid> customerIds)
        {
            this.customerIds = customerIds;
        }

        public override Expression<Func<Customer, bool>> Criteria
        {
            get
            {
                return customer => customerIds.Contains(customer.Id);
            }
        }
    }
}
