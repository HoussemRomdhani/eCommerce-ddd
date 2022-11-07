using eCommerce.Domain.Customers.Specifications;
using eCommerce.Domain.Customers;
using System;
using System.Threading;
using System.Threading.Tasks;
using eCommerce.Domain.Customers.Repositories;
using eCommerce.Application.Abstractions;
using eCommerce.Domain.SharedKernel.Results;

namespace eCommerce.Application.Customers.Commands.CreateCreditCard;

public class CreateCreditCardCommandHandler : ICommandHandler<CreateCreditCardCommand, Result>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCreditCardCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result> Handle(CreateCreditCardCommand request, CancellationToken cancellationToken)
    {
        var registeredSpec = new CustomerRegisteredSpec(request.CustomerId);

        var customer = await _customerRepository.FirstOrDefaultAsync(registeredSpec, cancellationToken);

        if (customer == null) 
            throw new Exception("No such customer exists");

        var creditCard = CreditCard.Create(customer, request.CreditCard.NameOnCard, request.CreditCard.CardNumber, request.CreditCard.Expiry);

        customer.Add(creditCard);

        await _customerRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
