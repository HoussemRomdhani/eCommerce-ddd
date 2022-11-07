using eCommerce.Application.Abstractions;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Customers.Repositories;
using eCommerce.Domain.Customers.Specifications;
using eCommerce.Domain.SharedKernel.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, Result>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
            throw new Exception("Id can't be empty");

        var registeredSpec = new CustomerRegisteredSpec(request.Id);

        var customer = await _customerRepository.FirstOrDefaultAsync(registeredSpec, cancellationToken);

        if (customer == null)
            throw new Exception("No such customer exists");

        customer.ChangeEmail(request.Customer.Email);

        await _customerRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
