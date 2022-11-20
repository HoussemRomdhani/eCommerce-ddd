using System;

namespace eCommerce.Application.Carts.Dtos.Requests;

public class AddProductToCartRequest 
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}