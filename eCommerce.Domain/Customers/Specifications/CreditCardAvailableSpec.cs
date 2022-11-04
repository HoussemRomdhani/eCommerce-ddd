using eCommerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace eCommerce.Domain.Customers.Specifications
{
    public class CreditCardAvailableSpec : SpecificationBase<CreditCard>
    {
        readonly DateTime dateTime;

        private CreditCardAvailableSpec(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public static CreditCardAvailableSpec Create(DateTime dateTime) => new CreditCardAvailableSpec(dateTime);

        public override Expression<Func<CreditCard, bool>> Criteria
        {
            get
            {
                return creditCard => creditCard.Active && creditCard.Expiry >= this.dateTime;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is CreditCardAvailableSpec spec &&
                   EqualityComparer<Expression<Func<CreditCard, bool>>>.Default.Equals(Criteria, spec.Criteria) &&
                   dateTime == spec.dateTime &&
                   EqualityComparer<Expression<Func<CreditCard, bool>>>.Default.Equals(Criteria, spec.Criteria);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Criteria, dateTime, Criteria);
        }
    }
}
