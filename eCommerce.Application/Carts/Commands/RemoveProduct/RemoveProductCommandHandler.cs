using eCommerce.Domain.Carts.Specifications;
using eCommerce.Domain.Carts;
using System.Threading;
using System.Threading.Tasks;
using System;
using eCommerce.Domain.Products;
using eCommerce.Domain.SharedKernel.Repositories;
using eCommerce.Domain.SharedKernel.Results;
using eCommerce.Domain.Common.DomainErrors;
using eCommerce.Application.Abstractions;

namespace eCommerce.Application.Carts.Commands.RemoveProduct;

public sealed class RemoveProductCommandHandler : ICommandHandler<RemoveProductCommand, Result>
{
    private readonly IReadRepositoryBase<Product> _productRepository;
    private readonly IRepositoryBase<Cart> _cartRepository;

    public RemoveProductCommandHandler(IReadRepositoryBase<Product> productRepository, IRepositoryBase<Cart> cartRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
    }

    public async Task<Result> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.FirstOrDefaultAsync(new CustomerCartSpec(request.CustomerId), cancellationToken);

        if (cart is null)
            return Result.Failure(DomainErrors.Cart.CartNotFoundForCustomer(request.CustomerId));

        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
            return Result.Failure(DomainErrors.Cart.ProductNotFoundInCart(request.ProductId));

         cart.Remove(product);

        await _cartRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
