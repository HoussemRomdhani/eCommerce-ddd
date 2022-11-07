using System;
using System.Collections.Generic;
using eCommerce.Application.Carts.Dtos.Requests;

namespace eCommerce.Application.Carts.Dtos.Responses;

public class CartResponseDto
{
    public Guid CustomerId { get; set; }
    public List<AddProductToCartRequest> Products { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
