using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using eCommerce.Domain.Services;
using System;
using System.Threading.Tasks;

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

    public static async Task<CartProduct> CreateAsync(Customer customer, Cart cart, Product product, int quantity, TaxService taxService)
    {
        var cartProduct = new CartProduct
        {
            CustomerId = customer.Id,
            CartId = cart.Id,
            ProductId = product.Id,
            Quantity = quantity,
            Created = DateTime.Now,
            Cost = product.Cost,
            Tax = await  taxService.CalculateAsync(customer, product)
        };

        return cartProduct;
    }
}
