using eCommerce.Application.Customers.Dtos.Responses;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;

namespace eCommerce.Application.Customers.Queries.GetCustomerById;

public sealed class GetCustomerByIdQuery : IRequest<Result<CustomerResponseDto>>
{
    public Guid Id { get; }

    private GetCustomerByIdQuery(Guid id)
    {
        Id = id;
    }

    public static GetCustomerByIdQuery Create(Guid id)
    {
        return new GetCustomerByIdQuery(id);
    }
}
