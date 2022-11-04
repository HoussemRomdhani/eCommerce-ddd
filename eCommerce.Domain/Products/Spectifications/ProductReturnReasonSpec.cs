using eCommerce.Domain.Core;
using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Products.Spectifications
{
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
                return product => true;
               // return product => product.Returns.ToList().Exists(returns => returns.Reason == returnReason);
            }
        }
    }
}
