using AutoMapper;
using eCommerce.Application.Abstractions;
using eCommerce.Application.Customers.Dtos.Responses;
using eCommerce.Domain.Customers;
using eCommerce.Domain.SharedKernel.Repositories;
using eCommerce.Domain.SharedKernel.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Customers.Queries.GetCustomerById;

public sealed class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, Result<CustomerDto>>
{
    private readonly IReadRepositoryBase<Customer> _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(IReadRepositoryBase<Customer> customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<Result<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);

        var result = _mapper.Map<Customer, CustomerDto>(customer);

        return Result.Success(result);
    }
}
