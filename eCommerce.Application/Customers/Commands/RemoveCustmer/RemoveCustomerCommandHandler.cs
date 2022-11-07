using eCommerce.Application.Abstractions;
using eCommerce.Domain.Customers.Repositories;
using eCommerce.Domain.Customers.Specifications;
using eCommerce.Domain.SharedKernel.Results;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Customers.Commands.RemoveCustmer;

public class RemoveCustomerCommandHandler : ICommandHandler<RemoveCustomerCommand, Result>
{
    private readonly ICustomerRepository _customerRepository;

    public RemoveCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
    {
        var registeredSpec = new CustomerRegisteredSpec(request.Id);

        var customer = await _customerRepository.FirstOrDefaultAsync(registeredSpec, cancellationToken);

        if (customer == null)
            throw new Exception("No such customer exists");

        await _customerRepository.DeleteAsync(customer, cancellationToken);

        await _customerRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
