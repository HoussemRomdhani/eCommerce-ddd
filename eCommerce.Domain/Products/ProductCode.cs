using eCommerce.Domain.SharedKernel;
using System;
using System.Collections.Generic;

namespace eCommerce.Domain.Products;

public class ProductCode : ValueObject
{
    public Guid Id { get; protected set; }
    public string Name { get; protected set; }

    public static ProductCode Create(string name)
    {
       return new ProductCode
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return Name;
    }
}