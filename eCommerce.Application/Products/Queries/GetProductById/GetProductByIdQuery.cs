
using eCommerce.Application.Products.Dtos.Responses;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;

namespace eCommerce.Application.Products.Queries.GetProductById;

public sealed class GetProductByIdQuery : IRequest<Result<ProductResponseDto>>
{
    public Guid Id { get;}

	private GetProductByIdQuery(Guid id)
	{
        Id = id;
    }

    public static GetProductByIdQuery Create(Guid id) 
    {
        return new GetProductByIdQuery(id);
    }
}
