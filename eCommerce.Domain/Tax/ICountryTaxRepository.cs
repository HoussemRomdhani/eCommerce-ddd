using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Domain.Tax;

public interface ICountryTaxRepository
{
    Task<CountryTax> GetCountryTaxAsync(Guid countryId, TaxType taxType, CancellationToken cancellationToken = default);
}
