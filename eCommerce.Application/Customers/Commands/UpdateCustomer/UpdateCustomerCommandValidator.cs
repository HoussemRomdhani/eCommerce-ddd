using FluentValidation;

namespace eCommerce.Application.Customers.Commands.UpdateCustomer;

public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
	public UpdateCustomerCommandValidator()
	{
        RuleFor(x => x.FirstName).NotNull()
                               .NotEmpty();

        RuleFor(x => x.LastName).NotNull()
                                .NotEmpty();

        RuleFor(x => x.Email).NotNull()
                             .NotEmpty()
                             .EmailAddress();
    }
}
