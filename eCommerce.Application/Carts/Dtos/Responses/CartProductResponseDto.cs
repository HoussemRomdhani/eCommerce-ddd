using System;

namespace eCommerce.Application.Carts.Dtos.Responses;

public class CartProductResponseDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public decimal Tax { get; set; }
    public DateTime Created { get; set; }
}
