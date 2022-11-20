using eCommerce.Application.Customers.Dtos.Responses;
using eCommerce.Domain.Customers;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Customers.Queries.GetAllCustomers;

public sealed class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, Result<IEnumerable<CustomerResponseDto>>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result<IEnumerable<CustomerResponseDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerRepository.GetAllAsync(cancellationToken);
       
        var result = CustomerDtoMapper.MapToCollection(customers);

        return Result.Success(result);
    }
}
