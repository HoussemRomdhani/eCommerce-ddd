using eCommerce.Application.Customers.Dtos.Responses;
using eCommerce.Domain.Customers;
using eCommerce.Domain.SharedKernel.Errors;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Customers.Queries.GetCustomerById;

public sealed class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Result<CustomerResponseDto>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result<CustomerResponseDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (customer is null)
            return Result.Failure<CustomerResponseDto>(Error.NotFound("GetCustomerById", $"Customer with id '{request.Id}' not found.")) ;

        var result = CustomerDtoMapper.Map(customer);

        return Result.Success(result);
    }
}
