using System;

namespace eCommerce.Application.Customers.Dtos.Responses;

public class CustomerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Guid CountryId { get; set; }
}