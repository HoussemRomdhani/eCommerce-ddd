using eCommerce.Domain.Core;
using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Carts.Specifications
{
    public class CustomerCartSpec : SpecificationBase<Cart>
    {
        private readonly Guid customerId;

        public CustomerCartSpec(Guid customerId)
        {
            this.customerId = customerId;
        }

        public override Expression<Func<Cart, bool>> Criteria
        {
            get
            {
                return cart => cart.CustomerId == customerId;
            }
        }
    }
}
