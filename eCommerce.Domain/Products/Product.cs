using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using eCommerce.Domain.SharedKernel;

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

    public ReadOnlyCollection<Return> Returns
    {
        get
        {
            return _returns.AsReadOnly();
        }
    }

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

    public void Rename(string newName)
    {
        if (Name == newName)
            return;
        
        Name = newName;
        Modified = DateTime.Now;
    }

    public void SetQuantity(int newQuantity)
    {
        if (Quantity == newQuantity)
            return;

        Quantity = newQuantity;
        Modified = DateTime.Now;
    }

    public void SetCost(decimal newcost)
    {
        if (Cost == newcost)
            return;

        Cost = newcost;
        Modified = DateTime.Now;
    }

    public void SetProductCode(ProductCode newCode)
    {
        if (Code == newCode)
            return;

        Code = newCode;
        Modified = DateTime.Now;
    }

}
