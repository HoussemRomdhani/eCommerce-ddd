using eCommerce.Domain.Carts;
using System.Threading;
using System.Threading.Tasks;
using System;
using eCommerce.Domain.Products;
using eCommerce.Domain.SharedKernel.Results;
using eCommerce.Domain.Common.DomainErrors;
using MediatR;

namespace eCommerce.Application.Carts.Commands.RemoveProduct;

public sealed class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, Result>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    public RemoveProductCommandHandler(ICartRepository cartRepository, IProductRepository productRepository)
    {
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<Result> Handle(RemoveProductCommand request, CancellationToken cancellationToken = default)
    {
        var cart = await _cartRepository.GetCartByCustomerIdAsync(request.CustomerId, cancellationToken);

        if (cart is null)
            return Result.Failure(DomainErrors.Cart.CartNotFoundForCustomer(request.CustomerId));

        var product = await _productRepository.GetProductByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
            return Result.Failure(DomainErrors.Cart.ProductNotFoundInCart(request.ProductId));

        cart.Remove(product);

        await _cartRepository.SaveAsync(cart, cancellationToken);

        return Result.Success();
    }
}
