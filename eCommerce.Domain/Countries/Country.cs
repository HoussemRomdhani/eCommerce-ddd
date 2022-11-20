using eCommerce.Domain.SharedKernel;
using System;

namespace eCommerce.Domain.Countries;

public class Country : EntityBase, IAggregateRoot
{
    public string Code { get; protected set; }
    public string Name { get; protected set; }

    public static Country Create(string code, string name) => Create(Guid.NewGuid(), code, name);

    public static Country Create(Guid id, string code, string name)
    {
        var country = new Country
        {
            Id = id,
            Code = code,
            Name = name
        };

       // DomainEvents.Raise(new CountryCreated { Country = country });
        
        return country;
    }
}
