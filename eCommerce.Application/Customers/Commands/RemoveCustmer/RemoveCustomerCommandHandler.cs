using eCommerce.Domain.Customers;
using eCommerce.Domain.SharedKernel.Errors;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Customers.Commands.RemoveCustmer;

public class RemoveCustomerCommandHandler : IRequestHandler<RemoveCustomerCommand, Result>
{
    private readonly ICustomerRepository _customerRepository;

    public RemoveCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    }

    public async Task<Result> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetCustomerByIdAsync(request.Id, cancellationToken);

        if (customer is null)
            return Result.Failure(Error.NotFound("CustomerNotFound", $"No such customer with '{request.Id}' exists"));

        await _customerRepository.DeleteCustomerAsync(customer, cancellationToken);

        await _customerRepository.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
