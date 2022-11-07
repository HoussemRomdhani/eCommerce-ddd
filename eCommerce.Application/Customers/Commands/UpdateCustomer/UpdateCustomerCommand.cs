using eCommerce.Application.Abstractions;
using eCommerce.Application.Customers.Dtos.Responses;
using eCommerce.Domain.SharedKernel.Results;
using System;

namespace eCommerce.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : ICommand<Result>
{
	public Guid Id { get;}
	public CustomerDto Customer { get; }
	public UpdateCustomerCommand(Guid id, CustomerDto customer)
	{
		Id = id;
		Customer = customer;
	}
}
