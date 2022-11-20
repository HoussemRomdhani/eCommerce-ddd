using eCommerce.Domain.Customers;
using eCommerce.Domain.SharedKernel.Errors;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Customers.Commands.UpdateCustomer;

public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Result>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (customer is null)
            return Result.Failure(Error.NotFound("CustomerNotFound", $"No such customer with '{request.Id}' exists"));

        var existingCustomerWithEmail = await _customerRepository.GetCustomerByEmailAsync(request.Email, cancellationToken);

        if (existingCustomerWithEmail is not null && customer.Id != existingCustomerWithEmail.Id)
            return Result.Failure(Error.Validation("EmailUnaivailable", $"No such email with '{request.Email}' available"));

        customer.ChangeFirstName(request.FirstName);
        customer.ChangeLastName(request.LastName);
        customer.ChangeEmail(request.Email);

        _customerRepository.UpdateCustomerAsync(customer, cancellationToken);

        await _customerRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
