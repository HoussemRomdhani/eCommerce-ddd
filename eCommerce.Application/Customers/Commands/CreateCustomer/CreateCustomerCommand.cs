using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;

namespace eCommerce.Application.Customers.Commands.CreateCustomer;

public sealed class CreateCustomerCommand : IRequest<Result<Guid>>
{
    public string FirstName { get;}
    public string LastName { get; }
    public string Email { get; }
    public string CountryCode { get;}

	private CreateCustomerCommand(CreateCustomerRequestDto dto)
	{
		FirstName = dto.FirstName;
		LastName = dto.LastName;
		Email = dto.Email;
		CountryCode = dto.CountryCode;
    }

    public static CreateCustomerCommand Create(CreateCustomerRequestDto dto)
    {
        return new CreateCustomerCommand(dto);
    }
}
