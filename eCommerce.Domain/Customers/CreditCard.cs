using eCommerce.Domain.Common.DomainErrors;
using eCommerce.Domain.SharedKernel;
using eCommerce.Domain.SharedKernel.Errors;
using eCommerce.Domain.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Domain.Customers;

public class CreditCard : ValueObject
{
    public string NameOnCard { get; protected set; }
    public string CardNumber { get; protected set; }
    public bool Active { get; protected set; }
    public DateTime Created { get; protected set; }
    public DateTime Expiry { get; protected set; }
    public Customer Customer { get; protected set; }

    public static Result<CreditCard> Create(Customer customer, string name, string cardNumber, DateTime expiry)
    {
        var errors = new List<Error>();

        if (string.IsNullOrEmpty(name))
            errors.Add(DomainErrors.Customer.CreditCardErrors.NameOnCardIsEmpty);

        if (string.IsNullOrEmpty(cardNumber))
            errors.Add(DomainErrors.Customer.CreditCardErrors.CardNumberIsEmpty);
        else if (cardNumber.Length < 6)
            errors.Add(DomainErrors.Customer.CreditCardErrors.CardNumberLengthIsIncorrect);

        if (DateTime.Now > expiry)
            errors.Add(DomainErrors.Customer.CreditCardErrors.CardExpiryInThePast);

        var creditCard = new CreditCard
        {
            Customer = customer,
            NameOnCard = name,
            CardNumber = cardNumber,
            Expiry = expiry,
            Active = true,
            Created = DateTime.Today
        };

        if (customer.CreditCards.Contains(creditCard))
            errors.Add(DomainErrors.Customer.CreditCardErrors.CardAlreadyExists);

        if (errors.Any())
            return Result.Failure<CreditCard>(errors);

        return Result.Success<CreditCard>(creditCard);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return NameOnCard;
        yield return CardNumber;
        yield return Active;
        yield return Created;
        yield return Expiry;
        yield return Customer.Id;
    }
}