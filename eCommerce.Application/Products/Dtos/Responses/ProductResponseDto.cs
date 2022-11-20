using eCommerce.Domain.Products;
using System;

namespace eCommerce.Application.Products.Dtos.Responses;

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public int Quantity { get; set; }
    public string Code { get; set; }
    public bool Active { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}
