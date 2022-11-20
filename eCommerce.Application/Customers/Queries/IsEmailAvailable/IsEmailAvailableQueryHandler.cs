using System.Threading;
using System.Threading.Tasks;
using System;
using eCommerce.Domain.SharedKernel.Results;
using eCommerce.Domain.Customers;
using MediatR;

namespace eCommerce.Application.Customers.Queries.IsEmailAvailable;

public sealed class IsEmailAvailableQueryHandler : IRequestHandler<IsEmailAvailableQuery, Result<bool>>
{
    private readonly ICustomerRepository _customerRepository;

    public IsEmailAvailableQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result<bool>> Handle(IsEmailAvailableQuery request, CancellationToken cancellationToken)
    {
        var existingCustomer = await _customerRepository.GetCustomerByEmailAsync(request.Email, cancellationToken);

        var isEmailAvailable = existingCustomer == null;

        return isEmailAvailable;
    }
}
