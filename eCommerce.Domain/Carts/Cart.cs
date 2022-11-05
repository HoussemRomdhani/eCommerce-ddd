using eCommerce.Domain.Carts.Specifications;
using eCommerce.Domain.Core;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace eCommerce.Domain.Carts;
public class Cart : IAggregateRoot
{
    private List<CartProduct> _cartProducts = new();
    public  Guid Id { get; protected set; }
    public Guid CustomerId { get; protected set; }
    public ReadOnlyCollection<CartProduct> Products => _cartProducts.AsReadOnly();
    public decimal TotalCost => Products.Sum(cartProduct => cartProduct.Quantity * cartProduct.Cost);
    public decimal TotalTax => Products.Sum(cartProducts => cartProducts.Tax);

    public static Cart Create(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));

        var cart = new Cart
        {
            Id = Guid.NewGuid(),
            CustomerId = customer.Id
        };

         DomainEvents.Raise(new CartCreated { Cart = cart });

        return cart;
    }

    public void Add(CartProduct cartProduct)
    {
        if (cartProduct == null)
            throw new ArgumentNullException();

        DomainEvents.Raise(new ProductAddedCart() { CartProduct = cartProduct });

        _cartProducts.Add(cartProduct);
    }
    public void Remove(Product product)
    {
        if (product == null)
            throw new ArgumentNullException("product");

        CartProduct cartProduct =
           _cartProducts.Find(new ProductInCartSpec(product).IsSatisfiedBy);

        DomainEvents.Raise<ProductRemovedCart>(new ProductRemovedCart() { CartProduct = cartProduct });

        _cartProducts.Remove(cartProduct);
    }
    public void Clear()
    {
        _cartProducts.Clear();
    }
}
