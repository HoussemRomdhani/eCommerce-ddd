using eCommerce.Domain.SharedKernel;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Customers.Specifications;
using eCommerce.Domain.Products;
using eCommerce.Domain.Tax;
using System;
using eCommerce.Domain.SharedKernel.Repositories;
using System.Threading.Tasks;

namespace eCommerce.Domain.Services;

public class TaxService : IDomainService
{
    private readonly Settings _settings;
    private readonly IReadRepositoryBase<CountryTax> _countryTaxRepository;

    public TaxService(Settings settings, IReadRepositoryBase<CountryTax> countryTaxRepository)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _countryTaxRepository = countryTaxRepository ?? throw new ArgumentNullException(nameof(countryTaxRepository));
    }

    public async Task<decimal> CalculateAsync(Customer customer, Product product)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));

        if (product == null)
            throw new ArgumentNullException(nameof(product));

        var customerCountryTax = await _countryTaxRepository.FirstOrDefaultAsync(CountryTypeOfTaxSpec.Create(customer.CountryId, TaxType.Customer));

        var businessCountryTax = await _countryTaxRepository.FirstOrDefaultAsync(CountryTypeOfTaxSpec.Create(_settings.BusinessCountry.Id, TaxType.Business));

        return (product.Cost * customerCountryTax.Percentage) + (product.Cost * businessCountryTax.Percentage);
    }
}
