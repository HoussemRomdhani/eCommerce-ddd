using eCommerce.Domain.Core;
using System;

namespace eCommerce.Domain.Products
{
    public class Category : IEntity
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        public static Category Create(string name)
        {
            return Create(Guid.NewGuid(), name);
        }

        public static Category Create(Guid id, string name) => new Category
        {
            Id = id,
            Name = name
        };
    }
}
