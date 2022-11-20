using eCommerce.Application.Core;
using eCommerce.Application.Core.Validation;
using FluentValidation;

namespace eCommerce.Application.Carts.Commands.Checkout;

public sealed class CheckoutCommandValidator : AbstractValidator<CheckoutCommand>
{
    public CheckoutCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty()
                                  .WithError(ValidationErrors.Cart.Checkout.CustomerIdIsRequired);
    }
}

