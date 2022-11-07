using System;

namespace eCommerce.Application.Customers.Dtos.Requests;

public class CreateCreditCardRequest
{
    public Guid Id { get; set; }
    public string NameOnCard { get; set; }
    public string CardNumber { get; set; }
    public DateTime Expiry { get; set; }
}