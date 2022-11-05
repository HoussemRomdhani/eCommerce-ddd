using eCommerce.Application.Products.Dtos;
using eCommerce.Domain.Products;
using LanguageExt;
using System;
using System.Collections.Generic;

namespace eCommerce.Application.Products.Services;

public interface IProductService
{
    IReadOnlyList<Product> GetAllProducts();
    Option<ProductDto> Get(Guid id);
    ProductDto Add(ProductDto product);
    void Update(ProductForUpdateDto product);
    void Remove(Guid id);
}
