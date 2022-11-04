using eCommerce.Domain.Core;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Customers.Specifications;
using eCommerce.Domain.Products;
using eCommerce.Domain.Tax;
using System;

namespace eCommerce.Domain.Services
{
    public class TaxService : IDomainService
    {
        private readonly IRepository<CountryTax> _countryTaxRepository;
        private readonly Settings _settings;

        public TaxService(Settings settings, IRepository<CountryTax> countryTaxRepository)
        {
            _countryTaxRepository = countryTaxRepository ?? throw new ArgumentNullException(nameof(countryTaxRepository));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public decimal Calculate(Customer customer, Product product)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            if (product == null)
                throw new ArgumentNullException("product");

            var customerCountryTax = _countryTaxRepository.FindOne(CountryTypeOfTaxSpec.Create(customer.CountryId, TaxType.Customer));
            var businessCountryTax = _countryTaxRepository.FindOne(CountryTypeOfTaxSpec.Create(_settings.BusinessCountry.Id, TaxType.Business));

           return (product.Cost * customerCountryTax.Percentage) + (product.Cost * businessCountryTax.Percentage);
        }
    }
}
