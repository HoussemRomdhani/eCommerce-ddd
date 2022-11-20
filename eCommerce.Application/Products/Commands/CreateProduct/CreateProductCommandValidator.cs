using FluentValidation;

namespace eCommerce.Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
	public CreateProductCommandValidator()
	{
		RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Cost).GreaterThan(0);
        RuleFor(x => x.Quantity).GreaterThan(0);
    }
}
