using eCommerce.Application.Products.Dtos.Responses;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System.Collections.Generic;

namespace eCommerce.Application.Products.Queries.GetAllProducts
{
    public sealed class GetAllProductsQuery : IRequest<Result<IEnumerable<ProductResponseDto>>>
    {
        public static GetAllProductsQuery Create() => new GetAllProductsQuery();
    }
}
