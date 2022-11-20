using eCommerce.Application.Carts.Dtos.Responses;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;

namespace eCommerce.Application.Carts.Queries.GetCart;

public sealed class GetCustomerCartQuery : IRequest<Result<CartResponseDto>>
{
    public Guid CustomerId { get; }

    private GetCustomerCartQuery(Guid customerId)
    {
        CustomerId = customerId;
    }

    public static GetCustomerCartQuery Create(Guid customerId)
    {
        return new GetCustomerCartQuery(customerId);
    }
}
