using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using eCommerce.Domain.Services;
using System;
namespace eCommerce.Domain.Carts;

public class CartProduct
{
    public Guid CartId { get; protected set; }
    public Guid CustomerId { get; protected set; }
    public int Quantity { get; protected set; }
    public Guid ProductId { get; protected set; }
    public DateTime Created { get; protected set; }
    public decimal Cost { get; protected set; }
    public decimal Tax { get; protected set; }

    public static CartProduct Create(Customer customer, Cart cart, Product product, int quantity, TaxService taxService)
    {
        if (cart == null) 
            throw new ArgumentNullException(nameof(cart));

        if (product == null) 
            throw new ArgumentNullException(nameof(product));

        var cartProduct = new CartProduct
        {
            CustomerId = customer.Id,
            CartId = cart.Id,
            ProductId = product.Id,
            Quantity = quantity,
            Created = DateTime.Now,
            Cost = product.Cost,
            Tax = taxService.Calculate(customer, product)
        };

        return cartProduct;
    }
}
