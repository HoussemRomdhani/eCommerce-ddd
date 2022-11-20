using eCommerce.Domain.Countries;
using eCommerce.Domain.Customers;
using System;
using System.Threading;
using System.Threading.Tasks;
using eCommerce.Domain.SharedKernel.Results;
using eCommerce.Application.Core;
using MediatR;

namespace eCommerce.Application.Customers.Commands.CreateCustomer;

public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result<Guid>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICountryRepository _countryRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, ICountryRepository countryRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
    }

    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = await _customerRepository.GetCustomerByEmailAsync(request.Email, cancellationToken);

        if (existingCustomer is not null)
            return Result.Failure<Guid>(ValidationErrors.Customer.CreateCustomer.EmailAlreayExists(request.Email));

        var country = await _countryRepository.GetCountryByCodeAsync(request.CountryCode, cancellationToken);

        if (country is null)
            return Result.Failure<Guid>(ValidationErrors.Customer.CreateCustomer.CountryNotFound(request.CountryCode));

        var customer = Customer.Create(request.FirstName, request.LastName, request.Email, country);

        await _customerRepository.AddCustomerAsync(customer, cancellationToken);

        await _customerRepository.SaveChangesAsync(cancellationToken);

        return Result.Success(customer.Id);
    }
}
