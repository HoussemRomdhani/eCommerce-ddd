﻿namespace eCommerce.Application.Products.Dtos.Requests;

public class UpdateProductRequestDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public string ProductCode { get; set; }
}