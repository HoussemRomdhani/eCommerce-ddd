using FluentValidation;

namespace eCommerce.Application.Customers.Commands.CreateCustomer;

public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.FirstName).NotNull()
                                 .NotEmpty();

        RuleFor(x => x.LastName).NotNull()
                                .NotEmpty();

        RuleFor(x => x.Email).NotNull()
                             .NotEmpty()
                             .EmailAddress();

        RuleFor(x => x.CountryCode).NotNull()
                                   .NotEmpty()
                                   .MinimumLength(3)
                                   .MaximumLength(3);
    }
}
