using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;

namespace eCommerce.Application.Carts.Commands.Checkout;

public sealed class CheckoutCommand : IRequest<Result>
{
    public Guid CustomerId { get; }

	private CheckoutCommand(Guid customerId)
	{
		CustomerId = customerId;
	}

	public static CheckoutCommand Create(Guid customerId)
	{
		return new CheckoutCommand(customerId);
	}
}
