using eCommerce.Domain.Products;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    private readonly IProductRepository _productRepository;
    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken = default)
    {
        var productCode = await _productRepository.GetProductCodeByNameAsync(request.ProductCodeName, cancellationToken);

        if (productCode is null)
            productCode = ProductCode.Create(request.ProductCodeName);

        var product = Product.Create(request.Name, request.Quantity, request.Cost, productCode);

        await _productRepository.AddProductAsync(product, cancellationToken);

        return Result.Success(product.Id);
    }
}
