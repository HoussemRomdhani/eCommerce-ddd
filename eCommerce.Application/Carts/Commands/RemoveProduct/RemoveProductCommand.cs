using eCommerce.Application.Abstractions;
using eCommerce.Application.Carts.Dtos;
using eCommerce.Domain.SharedKernel.Results;
using System;

namespace eCommerce.Application.Carts.Commands.RemoveProduct;

public sealed class RemoveProductCommand : ICommand<Result>
{
    public Guid CustomerId { get;}
    public Guid ProductId { get; }

    public RemoveProductCommand(Guid customerId, Guid productId)
    {
        CustomerId = customerId;
        ProductId = productId;
    }
}
