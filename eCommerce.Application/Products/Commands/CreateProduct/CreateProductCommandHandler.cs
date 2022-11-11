using eCommerce.Application.Abstractions;
using eCommerce.Domain.Products;
using eCommerce.Domain.SharedKernel.Repositories;
using eCommerce.Domain.SharedKernel.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Result>
{
    private readonly IRepositoryBase<Product> _productRepository;
    public CreateProductCommandHandler(IRepositoryBase<Product> productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(request.Name, request.Quantity, request.Cost, request.ProductCode);
        await _productRepository.AddAsync(product, cancellationToken);
        return Result.Success();
    }
}
