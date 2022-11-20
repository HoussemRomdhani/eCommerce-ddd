using eCommerce.Domain.Countries;
using eCommerce.Domain.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure;

public class InMemoryCountryTaxRepository : ICountryTaxRepository
{
    private List<CountryTax> _store = new()
    {
         CountryTax.Create(TaxType.Business, Country.Create("FRA", "France"), 20),
         CountryTax.Create(TaxType.Customer, Country.Create("FRA", "France"), 20),
         CountryTax.Create(TaxType.Business, Country.Create("FRA", "Germany"), 19),
         CountryTax.Create(TaxType.Customer, Country.Create("FRA", "Germany"), 19)
    };

    public Task<CountryTax> GetCountryTaxAsync(Guid countryId, TaxType taxType, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.FirstOrDefault(tax => tax.Country.Id == countryId && tax.Type == taxType));
    }
}
