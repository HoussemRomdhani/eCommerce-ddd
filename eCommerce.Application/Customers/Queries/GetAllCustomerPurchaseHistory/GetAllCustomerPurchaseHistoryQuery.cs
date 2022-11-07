using eCommerce.Application.Abstractions;
using eCommerce.Application.Customers.Dtos.Responses;
using System.Collections.Generic;

namespace eCommerce.Application.Customers.Queries.GetAllCustomerPurchaseHistory;

public class GetAllCustomerPurchaseHistoryQuery : IQuery<List<CustomerPurchaseHistoryDto>>
{
}
