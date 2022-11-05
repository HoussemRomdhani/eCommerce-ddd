﻿using eCommerce.Domain.Core;
namespace eCommerce.Domain.Carts;

public class ProductAddedCart : DomainEvent
{
    public CartProduct CartProduct { get; set; }

    public override void Flatten()
    {
        Args.Add("CartId", CartProduct.CartId);
        Args.Add("CustomerId", CartProduct.CustomerId);
        Args.Add("ProductId", CartProduct.ProductId);
        Args.Add("Quantity", CartProduct.Quantity);
    }
}