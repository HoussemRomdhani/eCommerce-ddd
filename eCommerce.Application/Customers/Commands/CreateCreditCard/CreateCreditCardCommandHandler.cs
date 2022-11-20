using eCommerce.Domain.Customers;
using System;
using System.Threading;
using System.Threading.Tasks;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using eCommerce.Domain.SharedKernel.Errors;

namespace eCommerce.Application.Customers.Commands.CreateCreditCard;

public sealed class CreateCreditCardCommandHandler : IRequestHandler<CreateCreditCardCommand, Result>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCreditCardCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result> Handle(CreateCreditCardCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (customer is null)
            return Result.Failure(Error.NotFound("CustomerNotFound", $"No such customer with '{request.Id}' exists"));

        var createCreditCardOperationResult = CreditCard.Create(customer, request.NameOnCard, request.CardNumber, request.Expiry);

        if (createCreditCardOperationResult.IsFailure)
            return Result.Failure(createCreditCardOperationResult.Errors);

        var creditCardValue = createCreditCardOperationResult.Value;

        customer.Add(creditCardValue);

        await _customerRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
