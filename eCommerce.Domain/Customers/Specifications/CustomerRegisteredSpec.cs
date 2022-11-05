using eCommerce.Domain.Core;
using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Customers.Specifications;

public class CustomerRegisteredSpec : SpecificationBase<Customer>
{
   private readonly Guid id;

    public CustomerRegisteredSpec(Guid id)
    {
        this.id = id;
    }

    public override Expression<Func<Customer, bool>> Criteria
    {
        get
        {
            return customer => customer.Id == id;
        }
    }
}
