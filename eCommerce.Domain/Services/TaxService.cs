using eCommerce.Domain.SharedKernel;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using eCommerce.Domain.Tax;
using System;
using System.Threading.Tasks;

namespace eCommerce.Domain.Services;

public class TaxService : IDomainService
{
    private readonly ICountryTaxRepository _countryTaxRepository;
    private readonly Settings _settings;

    public TaxService(ICountryTaxRepository countryTaxRepository, Settings settings)
    {
        _countryTaxRepository = countryTaxRepository ?? throw new ArgumentNullException(nameof(countryTaxRepository));
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public async Task<decimal> CalculateAsync(Customer customer, Product product)
    {
        if (customer is null)
            throw new ArgumentNullException(nameof(customer));

        if (product is null)
            throw new ArgumentNullException(nameof(product));

        var customerCountryTax = await _countryTaxRepository.GetCountryTaxAsync(customer.CountryId, TaxType.Customer);

        var businessCountryTax = await _countryTaxRepository.GetCountryTaxAsync(_settings.BusinessCountry.Id, TaxType.Business);

        return (product.Cost * customerCountryTax.Percentage) + (product.Cost * businessCountryTax.Percentage);
    }
}
