﻿using eCommerce.Domain.Core;

namespace eCommerce.Domain.Products;

public class ProductCodeCreated : DomainEvent
{
    public ProductCode ProductCode { get; set; }

    public override void Flatten()
    {
        Args.Add("ProductCodeId", ProductCode.Id);
        Args.Add("ProductCodeName", ProductCode.Name);
    }
}