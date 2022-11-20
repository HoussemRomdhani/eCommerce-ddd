using FluentValidation;

namespace eCommerce.Application.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
	public UpdateProductCommandValidator()
	{
		RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Cost).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
