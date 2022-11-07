using eCommerce.Domain.SharedKernel.Specifications;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace eCommerce.Domain.Products.Spectifications;

public class ProductReturnReasonSpec : SpecificationBase<Product>
{
    readonly ReturnReason returnReason;

    public ProductReturnReasonSpec(ReturnReason returnReason)
    {
        this.returnReason = returnReason;
    }

    public override Expression<Func<Product, bool>> Criteria
    {
        get
        {
           return product => product.Returns.ToList().Exists(returns => returns.Reason == returnReason);
        }
    }
}
