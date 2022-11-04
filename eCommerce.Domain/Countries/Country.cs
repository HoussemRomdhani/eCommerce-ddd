using eCommerce.Domain.Common;
using System;

namespace eCommerce.Domain.Countries
{
    public class Country : IEntity
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        public static Country Create(string name) => Create(Guid.NewGuid(), name);

        public static Country Create(Guid id, string name) => new Country
        {
            Id = id,
            Name = name
        };
    }
}
