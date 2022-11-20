using eCommerce.Domain.SharedKernel.Results;
using MediatR;

namespace eCommerce.Application.Customers.Queries.IsEmailAvailable;

public class IsEmailAvailableQuery : IRequest<Result<bool>>
{
    public string Email { get; }

	public IsEmailAvailableQuery(string email)
	{
		Email = email;
	}
}
