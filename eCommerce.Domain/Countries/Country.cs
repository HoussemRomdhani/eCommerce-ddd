using eCommerce.Domain.SharedKernel;
using System;

namespace eCommerce.Domain.Countries;

public class Country : EntityBase, IAggregateRoot
{
    public string Name { get; protected set; }
    public static Country Create(string name) => Create(Guid.NewGuid(), name);
    public static Country Create(Guid id, string name)
    {
        var country = new Country
        {
            Id = id,
            Name = name
        };

       // DomainEvents.Raise(new CountryCreated { Country = country });
        
        return country;
    }
}
