using eCommerce.Domain.Core;
using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Customers.Specifications
{
    public class CustomerAlreadyRegisteredSpec : SpecificationBase<Customer>
    {
       private readonly string email;

        public CustomerAlreadyRegisteredSpec(string email)
        {
            this.email = email;
        }

        public override Expression<Func<Customer, bool>> Criteria
        {
            get
            {
                return customer => customer.Email == email;
            }
        }
    }
}
