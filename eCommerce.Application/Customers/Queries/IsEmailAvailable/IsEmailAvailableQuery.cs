using eCommerce.Application.Abstractions;
using eCommerce.Domain.SharedKernel.Results;

namespace eCommerce.Application.Customers.Queries.IsEmailAvailable;

public class IsEmailAvailableQuery : IQuery<Result<bool>>
{
    public string Email { get; }

	public IsEmailAvailableQuery(string email)
	{
		Email = email;
	}
}
