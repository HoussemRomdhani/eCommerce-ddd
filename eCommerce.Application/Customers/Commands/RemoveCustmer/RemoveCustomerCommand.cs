using eCommerce.Application.Abstractions;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;

namespace eCommerce.Application.Customers.Commands.RemoveCustmer;

public class RemoveCustomerCommand : ICommand<Result>
{
    public Guid Id { get; }
	public RemoveCustomerCommand(Guid id)
	{
		Id = id;
	}
}
