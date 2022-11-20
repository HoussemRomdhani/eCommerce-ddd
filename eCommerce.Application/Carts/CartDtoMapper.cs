using eCommerce.Application.Carts.Dtos.Responses;
using eCommerce.Domain.Carts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace eCommerce.Application.Carts;

public static class CartDtoMapper
{
    public static CartResponseDto MapToCartResponseDto(Cart cart)
    {
        return new CartResponseDto
        {
            CustomerId = cart.CustomerId,
            Products = MapToCollectionOfCartProductResponseDto(cart.Products)
        };
    }

    private static IEnumerable<CartProductResponseDto> MapToCollectionOfCartProductResponseDto(ReadOnlyCollection<CartProduct> products)
    {
        return products?.Select(product => MapToCartProductResponseDto(product)) ?? 
               Enumerable.Empty<CartProductResponseDto>();
    }

    private static CartProductResponseDto MapToCartProductResponseDto(CartProduct product)
    {
        return new CartProductResponseDto
        {
            Id = product.ProductId,
            Cost = product.Cost,
            Quantity = product.Quantity,
            Tax = product.Tax,
            Created = product.Created
        };
    }
}
