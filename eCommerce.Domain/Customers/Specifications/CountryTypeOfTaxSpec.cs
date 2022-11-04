using eCommerce.Domain.Common;
using eCommerce.Domain.Tax;
using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Customers.Specifications
{
    public class CountryTypeOfTaxSpec : SpecificationBase<CountryTax>
    {
        private readonly Guid countryId;
        private readonly TaxType taxType;

        private CountryTypeOfTaxSpec(Guid countryId, TaxType taxType)
        {
            this.countryId = countryId;
            this.taxType = taxType;
        }

        public static CountryTypeOfTaxSpec Create(Guid countryId, TaxType taxType) => new CountryTypeOfTaxSpec(countryId, taxType);

        public override Expression<Func<CountryTax, bool>> Criteria
        {
            get
            {
                return countryTax => countryTax.Country.Id == countryId && countryTax.Type == taxType;
            }
        }

        public override bool Equals(object obj)
        {
            var countryTypeOfTaxSpecCompare = obj as CountryTypeOfTaxSpec;

            if (countryTypeOfTaxSpecCompare == null)
                throw new InvalidCastException("obj");

            return countryTypeOfTaxSpecCompare.countryId == countryId && countryTypeOfTaxSpecCompare.taxType == taxType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Criteria, countryId, taxType, Criteria);
        }
    }
}
