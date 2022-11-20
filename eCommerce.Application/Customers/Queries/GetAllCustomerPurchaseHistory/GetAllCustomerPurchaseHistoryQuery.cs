using eCommerce.Application.Customers.Dtos.Responses;
using MediatR;
using System.Collections.Generic;

namespace eCommerce.Application.Customers.Queries.GetAllCustomerPurchaseHistory;

public class GetAllCustomerPurchaseHistoryQuery : IRequest<List<CustomerPurchaseHistoryDto>>
{
}
