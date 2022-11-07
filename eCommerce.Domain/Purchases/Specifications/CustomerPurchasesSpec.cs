using eCommerce.Domain.SharedKernel.Specifications;
using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Purchases.Specifications;

public class CustomerPurchasesSpec : SpecificationBase<Purchase>
{
    private readonly Guid customerId;

    public CustomerPurchasesSpec(Guid customerId)
    {
        this.customerId = customerId;
    }

    public override Expression<Func<Purchase, bool>> Criteria
    {
        get
        {
            return purchase => purchase.CustomerId == customerId;
        }
    }
}
