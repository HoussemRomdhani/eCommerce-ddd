using eCommerce.Domain.Carts.Specifications;
using eCommerce.Domain.Carts;
using System;
using System.Threading;
using System.Threading.Tasks;
using eCommerce.Domain.Services;
using eCommerce.Domain.Customers;
using eCommerce.Domain.SharedKernel.Repositories;
using eCommerce.Domain.Common.DomainErrors;
using eCommerce.Domain.SharedKernel.Results;
using eCommerce.Application.Abstractions;

namespace eCommerce.Application.Carts.Commands.Checkout;

public sealed class CheckoutCommandHandler : ICommandHandler<CheckoutCommand, Result>
{
    private readonly IReadRepositoryBase<Customer> _customerRepository;
    private readonly IRepositoryBase<Cart> _cartRepository;
    private readonly CheckoutService _checkoutDomainService;

    public CheckoutCommandHandler(IReadRepositoryBase<Customer> customerRepository,
                                  IRepositoryBase<Cart> cartRepository,
                                  CheckoutService checkoutDomainService)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        _checkoutDomainService = checkoutDomainService ?? throw new ArgumentNullException(nameof(checkoutDomainService));
    }

    public async Task<Result> Handle(CheckoutCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.FirstOrDefaultAsync(new CustomerCartSpec(request.CustomerId), cancellationToken);

        if (cart is null)
            return Result.Failure(DomainErrors.Cart.CartNotFoundForCustomer(request.CustomerId));

        var customer = await _customerRepository.GetByIdAsync(cart.CustomerId, cancellationToken);

       var checkoutIssueResult = await _checkoutDomainService.CanCheckoutAsync(customer, cart);

        if (checkoutIssueResult.IsFailure)
            return Result.Failure(checkoutIssueResult.Error);

        var checkoutIssue = checkoutIssueResult.Value;

        if (checkoutIssue.HasValue)
        {
            var chekoutError = DomainErrors.Purchase.CheckoutIssue(checkoutIssue.Value.ToString());
            return Result.Failure(chekoutError);
        }

         _checkoutDomainService.Checkout(cart);

        return Result.Success();
    }
}
