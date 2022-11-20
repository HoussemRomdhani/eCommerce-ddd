using eCommerce.Application.Customers.Dtos.Responses;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System.Collections.Generic;

namespace eCommerce.Application.Customers.Queries.GetAllCustomers;

public sealed class GetAllCustomersQuery : IRequest<Result<IEnumerable<CustomerResponseDto>>>
{
    private GetAllCustomersQuery()
    {
    }

    public static GetAllCustomersQuery Create
    {
        get
        {
            return new GetAllCustomersQuery();
        }
    }
}
