using eCommerce.Application.Abstractions;
using eCommerce.Application.Carts.Dtos;
using eCommerce.Domain.SharedKernel.Results;
using System;

namespace eCommerce.Application.Carts.Commands.Checkout;

public sealed class CheckoutCommand : ICommand<Result>
{
    public Guid CustomerId { get; }

	public CheckoutCommand(Guid customerId)
	{
		CustomerId = customerId;
	}
}
