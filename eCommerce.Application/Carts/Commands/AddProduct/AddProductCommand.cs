using eCommerce.Application.Carts.Dtos.Requests;
using eCommerce.Domain.SharedKernel.Results;
using System;
using MediatR;

namespace eCommerce.Application.Carts.Commands.AddProduct;

public sealed class AddProductCommand : IRequest<Result>
{
    public Guid CustomerId { get; }
    public AddProductToCartRequest CartProduct { get; }

    private AddProductCommand(Guid customerId, AddProductToCartRequest cart)
    {
        CustomerId = customerId;
        CartProduct = cart;
    }

    public static AddProductCommand Create(Guid customerId, AddProductToCartRequest cart)
    {
        return new AddProductCommand(customerId, cart);
    }
}
