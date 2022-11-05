using eCommerce.Domain.Core;
using System.Linq.Expressions;
using System;

namespace eCommerce.Domain.Tax.Specifications;

public class CountryTypeOfTaxSpec : SpecificationBase<CountryTax>
{
    private readonly Guid countryId;
    private readonly TaxType taxType;

    public CountryTypeOfTaxSpec(Guid countryId, TaxType taxType)
    {
        this.countryId = countryId;
        this.taxType = taxType;
    }

    public override Expression<Func<CountryTax, bool>> Criteria
    {
        get
        {
            return countryTax => countryTax.Country.Id == countryId
                && countryTax.Type == taxType;
        }
    }

    public override bool Equals(object obj)
    {
        CountryTypeOfTaxSpec countryTypeOfTaxSpecCompare = obj as CountryTypeOfTaxSpec;

        if (countryTypeOfTaxSpecCompare == null)
            throw new InvalidCastException("obj");

        return countryTypeOfTaxSpecCompare.countryId == countryId &&
               countryTypeOfTaxSpecCompare.taxType == taxType;
    }

    public override int GetHashCode() => HashCode.Combine(countryId, taxType);
}
