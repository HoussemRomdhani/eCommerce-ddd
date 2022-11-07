using eCommerce.Application.Abstractions;
using eCommerce.Application.Carts.Dtos.Requests;
using eCommerce.Domain.SharedKernel.Results;
using System;

namespace eCommerce.Application.Carts.Commands.AddProduct;

public sealed class AddProductCommand : ICommand<Result>
{
    public Guid CustomerId { get; }
    public AddProductToCartRequest CartProduct { get; }

    public AddProductCommand(Guid customerId, AddProductToCartRequest cart)
    {
        CustomerId = customerId;
        CartProduct = cart;
    }
}
