using eCommerce.Domain.Carts.Specifications;
using eCommerce.Domain.Carts;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Domain.SharedKernel.Repositories;
using eCommerce.Domain.SharedKernel.Results;
using eCommerce.Domain.Common.DomainErrors;
using eCommerce.Application.Carts.Dtos.Responses;
using eCommerce.Application.Abstractions;

namespace eCommerce.Application.Carts.Queries.GetCart;

public class GetCustomerCartQueryHandler : IQueryHandler<GetCustomerCartQuery, Result<CartResponseDto>>
{
    private readonly IReadRepositoryBase<Cart> _cartRepository;
    private readonly IMapper _mapper;

    public GetCustomerCartQueryHandler(IReadRepositoryBase<Cart> cartRepository, IMapper mapper)
    {
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<Result<CartResponseDto>> Handle(GetCustomerCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.FirstOrDefaultAsync(new CustomerCartSpec(request.CustomerId), cancellationToken);

        if (cart is null)
            return Result.Failure<CartResponseDto>(DomainErrors.Cart.CartNotFoundForCustomer(request.CustomerId));

        var result = _mapper.Map<Cart, CartResponseDto>(cart);

        return Result.Success(result);
    }
}
