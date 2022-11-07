using eCommerce.Application.Abstractions;
using eCommerce.Domain.Carts;
using eCommerce.Domain.Carts.Specifications;
using eCommerce.Domain.Common.DomainErrors;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using eCommerce.Domain.Services;
using eCommerce.Domain.SharedKernel.Repositories;
using eCommerce.Domain.SharedKernel.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Carts.Commands.AddProduct;

public sealed class AddProductCommandHandler : ICommandHandler<AddProductCommand, Result>
{
    private readonly IReadRepositoryBase<Customer> _customerRepository;
    private readonly IReadRepositoryBase<Product> _productRepository;
    private readonly IRepositoryBase<Cart> _cartRepository;
    private readonly TaxService _taxDomainService;

    public AddProductCommandHandler(IReadRepositoryBase<Customer> customerRepository,
                                    IReadRepositoryBase<Product> productRepository,
                                    IRepositoryBase<Cart> cartRepository,
                                    TaxService taxDomainService)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _taxDomainService = taxDomainService ?? throw new ArgumentNullException(nameof(taxDomainService));
    }

    public async Task<Result> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);

        if (customer is null)
            return Result.Failure(DomainErrors.Customer.CustomerNotFound(request.CustomerId));

        var cart = await _cartRepository.FirstOrDefaultAsync(new CustomerCartSpec(request.CustomerId), cancellationToken);

        if (cart is null)
        {
            cart = Cart.Create(customer);

            await _cartRepository.AddAsync(cart, cancellationToken);
        }

        var product = await _productRepository.GetByIdAsync(request.CartProduct.ProductId, cancellationToken);

        if (product is null)
            return Result.Failure(DomainErrors.Product.CustomerNotFound(request.CustomerId));

        var cartProduct = await CartProduct.CreateAsync(customer, cart, product, request.CartProduct.Quantity, _taxDomainService);

        cart.Add(cartProduct);

        await _cartRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}