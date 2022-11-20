using System;

namespace eCommerce.Application.Customers.Dtos.Responses;

public class CreditCardResponseDto
{
    public string NameOnCard { get; set; }
    public string CardNumber { get; set; }
    public bool Active { get; set; }
    public DateTime Created { get; set; }
    public DateTime Expiry { get; set; }
}
