using eCommerce.Domain.SharedKernel;
using System;

namespace eCommerce.Domain.Products;

public class ProductCode : EntityBase, IAggregateRoot
{
    public string Name { get; protected set; }

    public static ProductCode Create(string name)
    {
        return Create(Guid.NewGuid(), name);
    }

    public static ProductCode Create(Guid id, string name)
    {
        var productCode = new ProductCode
        {
            Id = id,
            Name = name
        };

        return productCode;
    }
}