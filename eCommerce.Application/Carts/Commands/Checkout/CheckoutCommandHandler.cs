using eCommerce.Domain.Carts;
using System;
using System.Threading;
using System.Threading.Tasks;
using eCommerce.Domain.Services;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Common.DomainErrors;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;

namespace eCommerce.Application.Carts.Commands.Checkout;

public sealed class CheckoutCommandHandler : IRequestHandler<CheckoutCommand, Result>
{
    private readonly ICartRepository _cartRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly CheckoutService _checkoutDomainService;

    public CheckoutCommandHandler(ICartRepository cartRepository,
                                  ICustomerRepository customerRepository,
                                  CheckoutService checkoutDomainService)
    {
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _checkoutDomainService = checkoutDomainService ?? throw new ArgumentNullException(nameof(checkoutDomainService));
    }

    public async Task<Result> Handle(CheckoutCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartByCustomerIdAsync(request.CustomerId, cancellationToken);

        if (cart is null)
            return Result.Failure(DomainErrors.Cart.CartNotFoundForCustomer(request.CustomerId));

        var customer = await _customerRepository.GetCustomerByIdAsync(cart.CustomerId, cancellationToken);

       var checkoutIssueResult = await _checkoutDomainService.CanCheckoutAsync(customer, cart);

        if (checkoutIssueResult.IsFailure)
            return Result.Failure(checkoutIssueResult.Error);

        var checkoutIssue = checkoutIssueResult.Value;

        if (checkoutIssue.HasValue)
        {
            var chekoutError = DomainErrors.Purchase.CheckoutIssue(checkoutIssue.Value.ToString());
            return Result.Failure(chekoutError);
        }

       await _checkoutDomainService.Checkout(cart);

        return Result.Success();
    }
}
