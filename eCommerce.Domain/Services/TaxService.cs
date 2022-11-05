using eCommerce.Domain.Core;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Customers.Specifications;
using eCommerce.Domain.Products;
using eCommerce.Domain.Tax;
using System;

namespace eCommerce.Domain.Services;

public class TaxService : IDomainService
{
    private readonly Settings _settings;
    private readonly IRepository<CountryTax> _countryTaxRepository;

    public TaxService(Settings settings, IRepository<CountryTax> countryTaxRepository)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _countryTaxRepository = countryTaxRepository ?? throw new ArgumentNullException(nameof(countryTaxRepository));
    }

    public decimal Calculate(Customer customer, Product product)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));

        if (product == null)
            throw new ArgumentNullException(nameof(product));

        var customerCountryTax = _countryTaxRepository.FindOne(CountryTypeOfTaxSpec.Create(customer.CountryId, TaxType.Customer));

        var businessCountryTax = _countryTaxRepository.FindOne(CountryTypeOfTaxSpec.Create(_settings.BusinessCountry.Id, TaxType.Business));

        return (product.Cost * customerCountryTax.Percentage) + (product.Cost * businessCountryTax.Percentage);
    }
}
