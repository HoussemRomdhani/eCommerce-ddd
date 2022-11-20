namespace eCommerce.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string CountryCode { get; set; }
}
