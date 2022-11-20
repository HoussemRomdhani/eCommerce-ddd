using eCommerce.Domain.Carts;
using System;
using System.Threading;
using System.Threading.Tasks;
using eCommerce.Domain.SharedKernel.Results;
using eCommerce.Domain.Common.DomainErrors;
using eCommerce.Application.Carts.Dtos.Responses;
using MediatR;

namespace eCommerce.Application.Carts.Queries.GetCart;

public sealed class GetCustomerCartQueryHandler : IRequestHandler<GetCustomerCartQuery, Result<CartResponseDto>>
{
    private readonly ICartRepository _cartRepository;

    public GetCustomerCartQueryHandler(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
    }

    public async Task<Result<CartResponseDto>> Handle(GetCustomerCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartByCustomerIdAsync(request.CustomerId, cancellationToken);

        if (cart is null)
            return Result.Failure<CartResponseDto>(DomainErrors.Cart.CartNotFoundForCustomer(request.CustomerId));

        var result = CartDtoMapper.MapToCartResponseDto(cart);

        return Result.Success(result);
    }
}
