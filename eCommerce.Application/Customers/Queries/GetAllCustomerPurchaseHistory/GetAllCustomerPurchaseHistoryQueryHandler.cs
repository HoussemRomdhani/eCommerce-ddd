using eCommerce.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using eCommerce.Application.Customers.Dtos.Responses;
using MediatR;

namespace eCommerce.Application.Customers.Queries.GetAllCustomerPurchaseHistory;

public class GetAllCustomerPurchaseHistoryQueryHandler : IRequestHandler<GetAllCustomerPurchaseHistoryQuery, List<CustomerPurchaseHistoryDto>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomerPurchaseHistoryQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<List<CustomerPurchaseHistoryDto>> Handle(GetAllCustomerPurchaseHistoryQuery request, 
                                                               CancellationToken cancellationToken = default)
    {
        var customersPurchaseHistory = await _customerRepository.GetCustomersPurchaseHistoryAsync(cancellationToken);

        var result = CustomerDtoMapper.MapToCollectionOfCustomerPurchaseHistoryDto(customersPurchaseHistory);  

        return result;
    }
}
