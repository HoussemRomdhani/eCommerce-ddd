using eCommerce.Application.Core;
using eCommerce.Application.Core.Validation;
using FluentValidation;

namespace eCommerce.Application.Carts.Queries.GetCart;

public sealed class GetCustomerCartQueryValidator : AbstractValidator<GetCustomerCartQuery>
{
    public GetCustomerCartQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty()
                                  .WithError(ValidationErrors.GetCustomerCartQuery.CustomerIdIsRequired);
    }
}
