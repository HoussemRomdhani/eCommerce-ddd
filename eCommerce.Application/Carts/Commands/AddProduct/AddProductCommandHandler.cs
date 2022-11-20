using eCommerce.Domain.Carts;
using eCommerce.Domain.Common.DomainErrors;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using eCommerce.Domain.Services;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Carts.Commands.AddProduct;

public sealed class AddProductCommandHandler : IRequestHandler<AddProductCommand, Result>
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly TaxService _taxDomainService;

    public AddProductCommandHandler(ICartRepository cartRepository,
                                    IProductRepository productRepository,
                                    ICustomerRepository customerRepository,
                                    TaxService taxDomainService)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _taxDomainService = taxDomainService ?? throw new ArgumentNullException(nameof(taxDomainService));
    }

    public async Task<Result> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
            return Result.Failure(DomainErrors.Customer.CustomerNotFound(request.CustomerId));

        var cart = await _cartRepository.GetCartByCustomerIdAsync(request.CustomerId, cancellationToken);

        if (cart is null)
        {
            cart = Cart.Create(customer);
        }

        var product = await _productRepository.GetProductByIdAsync(request.CartProduct.ProductId, cancellationToken);

        if (product is null)
            return Result.Failure(DomainErrors.Product.CustomerNotFound(request.CustomerId));

        var cartProduct = await CartProduct.CreateAsync(customer, cart, product, request.CartProduct.Quantity, _taxDomainService);

        cart.Add(cartProduct);

        await _cartRepository.AddCartAsync(cart, cancellationToken);

        return Result.Success();
    }
}