using eCommerce.Application.Abstractions;
using eCommerce.Application.Carts.Dtos.Responses;
using eCommerce.Domain.SharedKernel.Results;
using System;

namespace eCommerce.Application.Carts.Queries.GetCart;

public class GetCustomerCartQuery : IQuery<Result<CartResponseDto>>
{
    public GetCustomerCartQuery(Guid customerId)
    {
        CustomerId = customerId;
    }

    public Guid CustomerId { get; }
}
