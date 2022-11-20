using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using eCommerce.Domain.Countries;
using eCommerce.Domain.SharedKernel;

namespace eCommerce.Domain.Customers;

public class Customer : IAggregateRoot
{
    private List<CreditCard> creditCards = new();
    public Guid Id { get; protected set; }
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public string Email { get; protected set; }
    public string Password { get; protected set; }
    public decimal Balance { get; protected set; }
    public Guid CountryId { get; protected set; }

    public ReadOnlyCollection<CreditCard> CreditCards => creditCards.AsReadOnly();

    public static Customer Create(string firstname, string lastname, string email, Country country)
    {
        return Create(Guid.NewGuid(), firstname, lastname, email, country);
    }
    private static Customer Create(Guid id, string firstname, string lastname, string email, Country country)
    {
        var customer = new Customer
        {
            Id = id,
            FirstName = firstname,
            LastName = lastname,
            Email = email,
            CountryId = country.Id
        };

    //  DomainEvents.Raise(new CustomerCreated { Customer = customer });

        return customer;
    }

    public void ChangeEmail(string email)
    {
        if (Email != email)
            Email = email;
    }

    public void ChangeFirstName(string firstName)
    {
        if (FirstName != firstName)
            FirstName = firstName;
    }

    public void ChangeLastName(string lastName)
    {
        if (LastName != lastName)
            LastName = lastName;
    }

    public ReadOnlyCollection<CreditCard> GetCreditCardsAvailble()
    {
        return null;
       // return creditCards.FindAll(CreditCardAvailableSpec.Create(DateTime.Today).IsSatisfiedBy).AsReadOnly();
    }

    public virtual void Add(CreditCard creditCard)
    {
        creditCards.Add(creditCard);
    }
}
