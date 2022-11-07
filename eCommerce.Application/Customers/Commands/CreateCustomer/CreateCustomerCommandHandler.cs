using eCommerce.Domain.Countries;
using eCommerce.Domain.Customers.Repositories;
using eCommerce.Domain.Customers.Specifications;
using eCommerce.Domain.Customers;
using System;
using System.Threading;
using System.Threading.Tasks;
using eCommerce.Domain.SharedKernel.Repositories;
using eCommerce.Domain.SharedKernel.Specifications;
using eCommerce.Application.Abstractions;
using eCommerce.Domain.SharedKernel.Results;

namespace eCommerce.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Result<Guid>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IReadRepositoryBase<Country> _countryRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IReadRepositoryBase<Country> countryRepository)
    {
        _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
    }

    public async Task<Result<Guid>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        ISpecification<Customer> alreadyRegisteredSpec = new CustomerAlreadyRegisteredSpec(request.Customer.Email);

        var existingCustomer = await _customerRepository.FirstOrDefaultAsync(alreadyRegisteredSpec, cancellationToken);

        if (existingCustomer != null)
            throw new Exception("Customer with this email already exists");

        var country = await _countryRepository.GetByIdAsync(request.Customer.CountryId, cancellationToken);

        var customer = Customer.Create(request.Customer.FirstName, request.Customer.LastName, request.Customer.Email, country);

        await _customerRepository.AddAsync(customer, cancellationToken);

        await _customerRepository.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}
