using eCommerce.Application.Core.Validation;
using eCommerce.Application.Core;
using FluentValidation;

namespace eCommerce.Application.Carts.Commands.RemoveProduct;

public sealed class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommand>
{
    public RemoveProductCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty()
                                 .WithError(ValidationErrors.Cart.RemoveProductFromCartRequest.CustomerIdIsRequired);

        RuleFor(x => x.ProductId).NotEmpty()
                                 .WithError(ValidationErrors.Cart.RemoveProductFromCartRequest.ProductIdIsRequired);
    }
}
