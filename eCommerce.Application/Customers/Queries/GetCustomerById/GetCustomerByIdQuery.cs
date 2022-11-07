using eCommerce.Application.Abstractions;
using eCommerce.Application.Customers.Dtos.Responses;
using eCommerce.Domain.SharedKernel.Results;
using System;

namespace eCommerce.Application.Customers.Queries.GetCustomerById
{
    public sealed class GetCustomerByIdQuery : IQuery<Result<CustomerDto>>
    {
        public Guid Id { get; }

        public GetCustomerByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
