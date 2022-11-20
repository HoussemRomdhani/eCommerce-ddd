using eCommerce.Domain.Carts;
using eCommerce.Domain.SharedKernel;
using System;

namespace eCommerce.Domain.Purchases;

public class PurchasedProduct :  IAggregateRoot
{
    public Guid Id { get; protected set; }
    public Guid ProductId { get; protected set; }
    public int Quantity { get; protected set; }

    public static PurchasedProduct Create(Purchase purchase, CartProduct cartProduct) => new PurchasedProduct
    {
        ProductId = cartProduct.ProductId,
        Id = purchase.Id,
        Quantity = cartProduct.Quantity
    };
}