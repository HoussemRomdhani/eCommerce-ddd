using eCommerce.Domain.Countries;
using eCommerce.Domain.SharedKernel;
using System;

namespace eCommerce.Domain.Tax;

public class CountryTax : EntityBase, IAggregateRoot
{
    public Country Country { get; protected set; }
    public decimal Percentage { get; protected set; }
    public TaxType Type { get; protected set; }

    public static CountryTax Create(TaxType type, Country country, decimal percentage) => Create(Guid.NewGuid(), type, country, percentage);

    public static CountryTax Create(Guid id, TaxType type, Country country, decimal percentage)
    {
        var countryTax = new CountryTax
        {
            Id = id,
            Country = country,
            Percentage = percentage,
            Type = type
        };

        return countryTax;
    }
}