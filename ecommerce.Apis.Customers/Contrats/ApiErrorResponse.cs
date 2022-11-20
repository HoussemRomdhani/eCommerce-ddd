using eCommerce.Domain.SharedKernel.Errors;
using System.Collections.Generic;

namespace ecommerce.Apis.Customers.Contrats
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(IReadOnlyCollection<Error> errors)
        {
            Errors = errors;
        }

        public IReadOnlyCollection<Error> Errors { get; }
    }
}
