using eCommerce.Domain.Core;
using System;

namespace eCommerce.Domain.Countries;

public class Country : IAggregateRoot
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; }

    public static Country Create(string name) => Create(Guid.NewGuid(), name);

    public static Country Create(Guid id, string name)
    {
        var country = new Country
        {
            Id = id,
            Name = name
        };

        DomainEvents.Raise(new CountryCreated { Country = country });
        
        return country;
    }
}
