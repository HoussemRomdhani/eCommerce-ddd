using eCommerce.Domain.Carts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using eCommerce.Domain.SharedKernel;

namespace eCommerce.Domain.Purchases;

public class Purchase : EntityBase, IAggregateRoot
{
    private List<PurchasedProduct> purchasedProducts = new();

    public ReadOnlyCollection<PurchasedProduct> Products
    {
        get { return purchasedProducts.AsReadOnly(); }
    }

    public DateTime Created { get; protected set; }
    public Guid CustomerId { get; protected set; }
    public decimal TotalTax { get; protected set; }
    public decimal TotalCost { get; protected set; }

    public static Purchase Create(Cart cart)
    {
        var purchase = new Purchase
        {
            Id = Guid.NewGuid(),
            Created = DateTime.Today,
            CustomerId = cart.CustomerId,
            TotalCost = cart.TotalCost,
            TotalTax = cart.TotalTax
        };

        var purchasedProducts = new List<PurchasedProduct>();

        foreach (CartProduct cartProduct in cart.Products)
        {
            var purchaseProduct = PurchasedProduct.Create(purchase, cartProduct);
            purchasedProducts.Add(purchaseProduct);
        }

        purchase.purchasedProducts = purchasedProducts;

        return purchase;
    }
}
