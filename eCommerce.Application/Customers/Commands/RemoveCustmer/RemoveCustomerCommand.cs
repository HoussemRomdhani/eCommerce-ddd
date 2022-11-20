using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;

namespace eCommerce.Application.Customers.Commands.RemoveCustmer;

public sealed class RemoveCustomerCommand : IRequest<Result>
{
    public Guid Id { get; }

	private RemoveCustomerCommand(Guid id)
	{
		Id = id;
	}

    public static RemoveCustomerCommand Create(Guid id)
    {
        return new RemoveCustomerCommand(id);
    }
}
