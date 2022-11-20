using eCommerce.Domain.SharedKernel.Errors;

namespace eCommerce.Application.Core;

internal static partial class ValidationErrors
{
    internal static class Cart
    {
        internal static class AddProductToCartRequest
        {
            internal static Error CustomerIdIsRequired => new("AddProductToCartRequest.CustomerIdIsRequired",
                                                              "The customer identifier is required.");

            internal static Error ProductIsRequired => new("AddProductToCartRequest.ProductIsRequired",
                                                           "The product is required.");

            internal static Error ProductIdIsRequired => new("AddProductToCartRequest.ProductIdIsRequired",
                                                             "The product identifier is required.");

            internal static Error QuantityMustBeGreaterThanZero => new("AddProductToCartRequest.QuantityMustBeGreaterThanZero",
                                                             "The quantity must be greater than zero.");
        }

        internal static class RemoveProductFromCartRequest
        {
            internal static Error CustomerIdIsRequired => new("RemoveProductFromCartRequest.CustomerIdIsRequired",
                                                              "The customer identifier is required.");

            internal static Error ProductIdIsRequired => new("RemoveProductFromCartRequest.ProductIdIsRequired",
                                                             "The product identifier is required.");
        }

        internal static class Checkout
        {
            internal static Error CustomerIdIsRequired => new("CheckoutRequest.CustomerIdIsRequired",
                                                             "The customer identifier is required.");
        }
    }
   
}
