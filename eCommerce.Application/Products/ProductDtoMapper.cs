using System.Collections.Generic;
using eCommerce.Domain.Products;
using System.Linq;
using eCommerce.Application.Products.Dtos.Responses;

namespace eCommerce.Application.Products;

internal static class ProductDtoMapper
{
    internal static ProductResponseDto Map(Product product)
    {
        var dto = new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Code = product.Code?.Name ?? string.Empty,
            Cost = product.Cost,
            Quantity = product.Quantity,
            Active = product.Active,
            Created = product.Created,
            Modified = product.Modified,
        };

        return dto;
    }

    internal static IEnumerable<ProductResponseDto> MapToCollection(IReadOnlyList<Product> products)
    {
        return products.Select(product => Map(product));
    }
}
