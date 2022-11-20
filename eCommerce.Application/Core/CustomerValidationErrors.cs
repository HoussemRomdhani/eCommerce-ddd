using eCommerce.Domain.SharedKernel.Errors;

namespace eCommerce.Application.Core;

internal static partial class ValidationErrors
{
    internal static class Customer
    {
        internal static class GetCustomerCartQuery
        {
            internal static Error CustomerIdIsRequired => new("GetCustomerCartQuery.CustomerIdIsRequired",
                                                           "The customer identifier is required.");
        }

        internal static class CreateCustomer
        {
            internal static Error CountryNotFound(string countryCode)
            {
                return new("CreateCustomer.CountryNotFound",
                            $"Country with this code : ({countryCode}) not found.");
            }

            internal static Error EmailAlreayExists(string email)
            {
                return new("CreateCustomer.EmailAlreayExists",
                           $"Customer with this email : ({email}) already exists.");
            }
        }
    }
}
