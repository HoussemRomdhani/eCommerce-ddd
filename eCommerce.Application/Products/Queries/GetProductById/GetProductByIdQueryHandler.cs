using eCommerce.Application.Products.Dtos.Responses;
using eCommerce.Domain.Products;
using eCommerce.Domain.SharedKernel.Errors;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Products.Queries.GetProductById;

public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductResponseDto>>
{
    private readonly IProductRepository _productRepository;
    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<ProductResponseDto>> Handle(GetProductByIdQuery request,CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);

        if (product is null)
        {
            var error = Error.NotFound("GetProductById", $"Product with id '{request.Id}' not found.");
            return Result.Failure<ProductResponseDto>(error);
        }

        var response = ProductDtoMapper.Map(product);

        return Result.Success(response);
    }
}
