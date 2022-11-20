using eCommerce.Application.Customers.Dtos.Responses;
using eCommerce.Domain.Customers;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Application.Customers;

internal static class CustomerDtoMapper
{
    internal static CustomerResponseDto Map(Customer customer)
    {
        var dto = new CustomerResponseDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Cards = CreditCardDtoMapper.MapToCollection(customer.CreditCards)
        };

        return dto;
    }

    internal static IEnumerable<CustomerResponseDto> MapToCollection(IReadOnlyList<Customer> customers)
    {
       return customers.Select(customer => Map(customer));
    }
}

internal static class CreditCardDtoMapper
{
    private static CreditCardResponseDto Map(CreditCard card)
    {
        var dto = new CreditCardResponseDto
        {
            NameOnCard = card.NameOnCard,
            CardNumber = card.CardNumber,
            Active = card.Active,
            Created = card.Created,
            Expiry = card.Expiry,
        };

        return dto;
    }

    internal static IEnumerable<CreditCardResponseDto> MapToCollection(IReadOnlyList<CreditCard> cards)
    {
        return cards.Select(card => Map(card));
    }
}

