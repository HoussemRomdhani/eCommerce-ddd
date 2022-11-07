using eCommerce.Application.Abstractions;
using eCommerce.Application.Customers.Dtos.Responses;
using eCommerce.Domain.SharedKernel.Results;
using System;

namespace eCommerce.Application.Customers.Commands.CreateCustomer;

public sealed class CreateCustomerCommand : ICommand<Result<Guid>>
{
    public CustomerDto Customer { get; }

	public CreateCustomerCommand(CustomerDto customer)
	{
		Customer = customer;
    }
}
