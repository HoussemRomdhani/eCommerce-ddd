using eCommerce.Domain.Products;
using eCommerce.Domain.SharedKernel.Errors;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result>
{
    private readonly IProductRepository _productRepository;
    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetProductByIdAsync(request.Id, cancellationToken);

        if (product is null)
            return Result.Failure(Error.NotFound("ProductNotFound", $"No such product with '{request.Id}' exists"));

        var productCode = await _productRepository.GetProductCodeByNameAsync(request.ProductCodeName, cancellationToken);

        if (productCode is null)
            productCode = ProductCode.Create(request.ProductCodeName);

        product.Rename(request.Name);

        product.SetQuantity(request.Quantity);

        product.SetCost(request.Cost);

        product.SetProductCode(productCode);

        await _productRepository.UpdateProductAsync(product, cancellationToken);

        return Result.Success();
    }
}
