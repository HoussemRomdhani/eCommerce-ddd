using eCommerce.Domain.Core;
using System;

namespace eCommerce.Domain.Products;

public class ProductCode : IAggregateRoot
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; }

    public static ProductCode Create(string name)
    {
        return Create(Guid.NewGuid(), name);
    }

    public static ProductCode Create(Guid id, string name)
    {
        ProductCode productCode = new ProductCode()
        {
            Id = id,
            Name = name
        };

        DomainEvents.Raise(new ProductCodeCreated { ProductCode = productCode });

        return productCode;
    }
}