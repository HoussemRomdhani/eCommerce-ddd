using AutoMapper;
using eCommerce.Domain.Core;
using eCommerce.Domain.Countries;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Customers.Repositories;
using eCommerce.Domain.Customers.Specifications;
using eCommerce.Domain.Purchases;
using eCommerce.Domain.Purchases.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Application.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Purchase> _purchaseRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository,
            IRepository<Country> countryRepository, IRepository<Purchase> purchaseRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _purchaseRepository = purchaseRepository ?? throw new ArgumentNullException(nameof(purchaseRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public bool IsEmailAvailable(string email)
        {
            ISpecification<Customer> alreadyRegisteredSpec = new CustomerAlreadyRegisteredSpec(email);

            Customer existingCustomer = _customerRepository.FindOne(alreadyRegisteredSpec);

            if (existingCustomer == null)
                return true;

            return false;
        }

        public CustomerDto Add(CustomerDto customerDto)
        {
            ISpecification<Customer> alreadyRegisteredSpec = new CustomerAlreadyRegisteredSpec(customerDto.Email);

            Customer existingCustomer = _customerRepository.FindOne(alreadyRegisteredSpec);

            if (existingCustomer != null) throw new Exception("Customer with this email already exists");

            Country country = _countryRepository.FindById(customerDto.CountryId);

            Customer customer = Customer.Create(customerDto.FirstName, customerDto.LastName, customerDto.Email, country);

            _customerRepository.Add(customer);
            
            _unitOfWork.Commit();

            return _mapper.Map<Customer, CustomerDto>(customer);
        }

        public void Update(CustomerDto customerDto)
        {
            if (customerDto.Id == Guid.Empty)  throw new Exception("Id can't be empty");

            ISpecification<Customer> registeredSpec =  new CustomerRegisteredSpec(customerDto.Id);

            Customer customer = _customerRepository.FindOne(registeredSpec);

            if (customer == null) throw new Exception("No such customer exists");

            customer.ChangeEmail(customerDto.Email);

            _unitOfWork.Commit();
        }

        public void Remove(Guid customerId)
        {
            ISpecification<Customer> registeredSpec = new CustomerRegisteredSpec(customerId);

            Customer customer = _customerRepository.FindOne(registeredSpec);

            if (customer == null) throw new Exception("No such customer exists");

            _customerRepository.Remove(customer);

            _unitOfWork.Commit();
        }

        public CustomerDto Get(Guid customerId)
        {
            ISpecification<Customer> registeredSpec = new CustomerRegisteredSpec(customerId);

            Customer customer = _customerRepository.FindOne(registeredSpec);

            return _mapper.Map<Customer, CustomerDto>(customer);
        }


        public CreditCardDto Add(Guid customerId, CreditCardDto creditCardDto)
        {
            ISpecification<Customer> registeredSpec = new CustomerRegisteredSpec(customerId);

            Customer customer = _customerRepository.FindOne(registeredSpec);

            if (customer == null) throw new Exception("No such customer exists");

            var creditCard = CreditCard.Create(customer, creditCardDto.NameOnCard, creditCardDto.CardNumber, creditCardDto.Expiry);

            customer.Add(creditCard);

            _unitOfWork.Commit();

            return _mapper.Map<CreditCard, CreditCardDto>(creditCard);
        }

        //Approach 1 - Domain Model DTO Projection 
        public List<CustomerPurchaseHistoryDto> GetAllCustomerPurchaseHistoryV1()
        {
            IEnumerable<Guid> customersThatHavePurhcasedSomething =
                 _purchaseRepository.Find(new PurchasedNProductsSpec(1))
                                        .Select(purchase => purchase.CustomerId)
                                        .Distinct();

            IEnumerable<Customer> customers = _customerRepository.Find(new CustomerBulkIdFindSpec(customersThatHavePurhcasedSomething));

            var customersPurchaseHistory = new List<CustomerPurchaseHistoryDto>();

            foreach (Customer customer in customers)
            {
                IEnumerable<Purchase> customerPurchases = _purchaseRepository.Find(new CustomerPurchasesSpec(customer.Id));

                CustomerPurchaseHistoryDto customerPurchaseHistory = new CustomerPurchaseHistoryDto();

                customerPurchaseHistory.CustomerId = customer.Id;
                customerPurchaseHistory.FirstName = customer.FirstName;
                customerPurchaseHistory.LastName = customer.LastName;
                customerPurchaseHistory.Email = customer.Email;
                customerPurchaseHistory.TotalPurchases = customerPurchases.Count();
                customerPurchaseHistory.TotalProductsPurchased =
                    customerPurchases.Sum(purchase => purchase.Products.Sum(product => product.Quantity));
                customerPurchaseHistory.TotalCost = customerPurchases.Sum(purchase => purchase.TotalCost);
                customersPurchaseHistory.Add(customerPurchaseHistory);

            }
            return customersPurchaseHistory;
        }

        //Approach 2 - Infrastructure Read Model Projection (Preferred)
        public List<CustomerPurchaseHistoryDto> GetAllCustomerPurchaseHistoryV2()
        {
            IEnumerable<CustomerPurchaseHistoryReadModel> customersPurchaseHistory = _customerRepository.GetCustomersPurchaseHistory();

            return _mapper.Map<IEnumerable<CustomerPurchaseHistoryReadModel>, List<CustomerPurchaseHistoryDto>>(customersPurchaseHistory);
        }
    }
}
