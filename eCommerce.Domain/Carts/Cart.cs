using eCommerce.Domain.Carts.Specifications;
using eCommerce.Domain.Common.DomainErrors;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using eCommerce.Domain.SharedKernel;
using eCommerce.Domain.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace eCommerce.Domain.Carts;

public class Cart : IAggregateRoot
{
    private List<CartProduct> _cartProducts = new();
    public Guid Id { get; protected set; }
    public Guid CustomerId { get; protected set; }
    public ReadOnlyCollection<CartProduct> Products => _cartProducts.AsReadOnly();
    public decimal TotalCost => Products.Sum(cartProduct => cartProduct.Quantity * cartProduct.Cost);
    public decimal TotalTax => Products.Sum(cartProducts => cartProducts.Tax);


    public static Cart Create(Customer customer) => new()
    {
        Id = Guid.NewGuid(),
        CustomerId = customer.Id
    };

    public Result Add(CartProduct cartProduct)
    {
        if (cartProduct == null)
            return Result.Failure(DomainErrors.Cart.CartProductIsNull);

        _cartProducts.Add(cartProduct);

        return Result.Success();
    }

    public Result Remove(Product product)
    {
        if (product == null)
            return Result.Failure(DomainErrors.Cart.ProductInCartIsNull);

        var cartProduct = _cartProducts.Find(new ProductInCartSpec(product).IsSatisfiedBy);

        _cartProducts.Remove(cartProduct);

        return Result.Success();
    }

    public Result Clear()
    {
        _cartProducts.Clear();
        return Result.Success();
    }
}
