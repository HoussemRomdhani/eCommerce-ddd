using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;

namespace eCommerce.Application.Carts.Commands.RemoveProduct;

public sealed class RemoveProductCommand : IRequest<Result>
{
    public Guid CustomerId { get;}

    public Guid ProductId { get; }

    private RemoveProductCommand(Guid customerId, Guid productId)
    {
        CustomerId = customerId;
        ProductId = productId;
    }

    public static RemoveProductCommand Create(Guid customerId, Guid productId)
    {
        return new RemoveProductCommand(customerId, productId);
    }
}
