using System;
using System.Collections.Generic;

namespace eCommerce.Application.Carts.Dtos.Responses;

public class CartResponseDto
{
    public Guid CustomerId { get; set; }
    public IEnumerable<CartProductResponseDto> Products { get; set; }
}
