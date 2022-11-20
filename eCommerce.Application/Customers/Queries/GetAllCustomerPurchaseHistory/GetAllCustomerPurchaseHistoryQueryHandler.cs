using eCommerce.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Application.Customers.Dtos.Responses;
using MediatR;

namespace eCommerce.Application.Customers.Queries.GetAllCustomerPurchaseHistory;

public class GetAllCustomerPurchaseHistoryQueryHandler : IRequestHandler<GetAllCustomerPurchaseHistoryQuery, List<CustomerPurchaseHistoryDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetAllCustomerPurchaseHistoryQueryHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public  Task<List<CustomerPurchaseHistoryDto>> Handle(GetAllCustomerPurchaseHistoryQuery request, CancellationToken cancellationToken)
    {
        var customersPurchaseHistory = _customerRepository.GetCustomersPurchaseHistory();
        
        var result = _mapper.Map<IEnumerable<CustomerPurchaseHistoryReadModel>, List<CustomerPurchaseHistoryDto>>(customersPurchaseHistory);

        return Task.FromResult(result);
    }
}
