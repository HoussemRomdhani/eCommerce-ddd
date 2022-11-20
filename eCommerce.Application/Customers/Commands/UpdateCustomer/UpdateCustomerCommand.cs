using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;

namespace eCommerce.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommand : IRequest<Result>
{
	public Guid Id { get;}
    public string FirstName { get;}
    public string LastName { get;}
    public string Email { get;}

	private UpdateCustomerCommand(Guid id, UpdateCustomerRequestDto dto)
	{
		Id = id;
		FirstName = dto.FirstName;
		LastName = dto.LastName;
		Email = dto.Email;
	}

    public static UpdateCustomerCommand Create(Guid id, UpdateCustomerRequestDto dto)
    {
        return new UpdateCustomerCommand(id, dto);
    }
}
