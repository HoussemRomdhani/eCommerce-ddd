using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using eCommerce.Domain.SharedKernel;

namespace eCommerce.Domain.Products;

public class Product : EntityBase, IAggregateRoot
{
    private List<Return> _returns = new();

    public string Name { get; protected set; }
    public ProductCode Code { get; protected set; }
    public DateTime Created { get; protected set; }
    public DateTime Modified { get; protected set; }
    public bool Active { get; protected set; }
    public int Quantity { get; protected set; }
    public decimal Cost { get; protected set; }

    public ReadOnlyCollection<Return> Returns => _returns.AsReadOnly();

    public static Product Create(string name, int quantity, decimal cost, ProductCode productCode)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Quantity = quantity,
            Created = DateTime.Now,
            Modified = DateTime.Now,
            Active = true,
            Cost = cost,
            Code = productCode
        };

        return product;
    }
}
