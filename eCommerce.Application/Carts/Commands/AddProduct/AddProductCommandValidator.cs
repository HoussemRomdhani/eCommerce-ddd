using eCommerce.Application.Core;
using eCommerce.Application.Core.Validation;
using FluentValidation;

namespace eCommerce.Application.Carts.Commands.AddProduct;

public sealed class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty()
                                  .WithError(ValidationErrors.AddProductToCartRequest.CustomerIdIsRequired);

        RuleFor(x => x.CartProduct).NotNull()
                                  .WithError(ValidationErrors.AddProductToCartRequest.ProductIsRequired);

        RuleFor(x => x.CartProduct.ProductId).NotEmpty()
                                 .WithError(ValidationErrors.AddProductToCartRequest.ProductIdIsRequired);

        RuleFor(x => x.CartProduct.Quantity).GreaterThan(0)
                                 .WithError(ValidationErrors.AddProductToCartRequest.QuantityMustBeGreaterThanZero);
    }
}
