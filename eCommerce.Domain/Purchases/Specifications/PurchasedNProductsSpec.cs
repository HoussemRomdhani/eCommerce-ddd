using eCommerce.Domain.Core;
using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Purchases.Specifications;

public class PurchasedNProductsSpec : SpecificationBase<Purchase>
{
   private readonly int _nProducts;

    public PurchasedNProductsSpec(int nProducts)
    {
        _nProducts = nProducts;
    }

    public override Expression<Func<Purchase, bool>> Criteria
    {
        get
        {
            return purchase => purchase.Products.Count >= _nProducts;
        }
    }
}
