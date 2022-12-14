using eCommerce.Domain.Common;
using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Purchases.Specifications
{
    public class PurchasedNProductsSpec : SpecificationBase<Purchase>
    {
       private readonly int nProducts;

        public PurchasedNProductsSpec(int nProducts)
        {
            this.nProducts = nProducts;
        }

        public override Expression<Func<Purchase, bool>> Criteria
        {
            get
            {
                return purchase => purchase.Products.Count >= nProducts;
            }
        }
    }
}
