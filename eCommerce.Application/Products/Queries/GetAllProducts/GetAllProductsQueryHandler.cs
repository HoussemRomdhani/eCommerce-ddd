using eCommerce.Application.Products.Dtos.Responses;
using eCommerce.Domain.Products;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Products.Queries.GetAllProducts;

public sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<ProductResponseDto>>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<IEnumerable<ProductResponseDto>>> Handle(GetAllProductsQuery request, 
                                                                CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);

        var result = ProductDtoMapper.MapToCollection(products);

        return Result.Success(result);
    }
}
