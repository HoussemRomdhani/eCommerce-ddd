using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using eCommerce.Domain.Core;

namespace eCommerce.Domain.Products;

public class Product : IAggregateRoot
{
    private List<Return> _returns = new();

    public Guid Id { get; protected set; }
    public string Name { get; protected set; }
    public ProductCode Code { get; protected set; }
    public DateTime Created { get; protected set; }
    public DateTime Modified { get; protected set; }
    public bool Active { get; protected set; }
    public int Quantity { get; protected set; }
    public decimal Cost { get; protected set; }
    public Category Category { get; protected set; }

    public ReadOnlyCollection<Return> Returns => _returns.AsReadOnly();

    public static Product Create(string name, int quantity, decimal cost, Category category, ProductCode productCode) =>
                          Create(Guid.NewGuid(), name, quantity, cost, category, productCode);

    public static Product Create(Guid id, 
                                 string name, 
                                 int quantity, 
                                 decimal cost, 
                                 Category category, 
                                 ProductCode productCode)
    {
        var product = new Product
        {
            Id = id,
            Name = name,
            Quantity = quantity,
            Created = DateTime.Now,
            Modified = DateTime.Now,
            Active = true,
            Cost = cost,
            Category = category,
            Code = productCode
        };

        DomainEvents.Raise(new ProductCreated { Product = product });

        return product;
    }
}
