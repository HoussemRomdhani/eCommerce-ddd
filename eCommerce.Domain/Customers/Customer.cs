using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using eCommerce.Domain.Countries;
using eCommerce.Domain.Customers.Specifications;
using eCommerce.Domain.Common;

namespace eCommerce.Domain.Customers
{
    public class Customer : IEntity
    {
        private List<CreditCard> creditCards = new List<CreditCard>();

        public Guid Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public decimal Balance { get; protected set; }
        public Guid CountryId { get; protected set; }
        public ReadOnlyCollection<CreditCard> CreditCards { get { return creditCards.AsReadOnly(); } }
        public void ChangeEmail(string email)
        {
            if (Email != email)
            {
                Email = email;
              //  DomainEvents.Raise<CustomerChangedEmail>(new CustomerChangedEmail() { Customer = this });
            }
        }

        public static Customer Create(string firstname, string lastname, string email, Country country) => Create(Guid.NewGuid(), firstname, lastname, email, country); 
       
        public static Customer Create(Guid id, string firstname, string lastname, string email, Country country)
        {
            if (string.IsNullOrEmpty(firstname))
                throw new ArgumentNullException("firstname");

            if (string.IsNullOrEmpty(lastname))
                throw new ArgumentNullException("lastname");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException("email");

            if (country == null)
                throw new ArgumentNullException("country");

            var customer = new Customer
            {
                Id = id,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                CountryId = country.Id
            };

        //    DomainEvents.Raise<CustomerCreated>(new CustomerCreated() { Customer = customer });

            return customer;
        }

        public ReadOnlyCollection<CreditCard> GetCreditCardsAvailble()
        {
            return creditCards.FindAll(CreditCardAvailableSpec.Create(DateTime.Today).IsSatisfiedBy).AsReadOnly();
        }

        public virtual void Add(CreditCard creditCard)
        {
            creditCards.Add(creditCard);

           // DomainEvents.Raise<CreditCardAdded>(new CreditCardAdded() { CreditCard = creditCard });
        }
    }
}
