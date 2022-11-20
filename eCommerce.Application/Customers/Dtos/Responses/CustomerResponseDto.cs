using System;
using System.Collections.Generic;

namespace eCommerce.Application.Customers.Dtos.Responses;

public class CustomerResponseDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public IEnumerable<CreditCardResponseDto> Cards { get; set; }
}