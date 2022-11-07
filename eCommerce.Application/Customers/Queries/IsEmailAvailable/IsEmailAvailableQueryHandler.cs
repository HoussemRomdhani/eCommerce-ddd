using eCommerce.Domain.Customers.Repositories;
using eCommerce.Domain.Customers.Specifications;
using System.Threading;
using System.Threading.Tasks;
using System;
using eCommerce.Application.Abstractions;
using eCommerce.Domain.SharedKernel.Results;

namespace eCommerce.Application.Customers.Queries.IsEmailAvailable;

public sealed class IsEmailAvailableQueryHandler : IQueryHandler<IsEmailAvailableQuery, Result<bool>>
{
    private readonly ICustomerRepository _customerRepository;

    public IsEmailAvailableQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result<bool>> Handle(IsEmailAvailableQuery request, CancellationToken cancellationToken)
    {
        var alreadyRegisteredSpec = new CustomerAlreadyRegisteredSpec(request.Email);

        var existingCustomer = await _customerRepository.FirstOrDefaultAsync(alreadyRegisteredSpec, cancellationToken);

        var isEmailAvailable = existingCustomer == null;

        return isEmailAvailable;
    }
}
