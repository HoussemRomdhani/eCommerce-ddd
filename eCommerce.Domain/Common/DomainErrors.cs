﻿using eCommerce.Domain.SharedKernel.Errors;
using System;

namespace eCommerce.Domain.Common.DomainErrors;

public static class DomainErrors
{
    public static class Cart
    {
        public static readonly Error ProductInCartIsNull = new("ProductInCartIsNull", "Product in cart can't be null");
        public static readonly Error CustomerIsNull = new("CustomerIsNull", "Customer in cart can't be null");
        public static readonly Error CartProductIsNull = new("CartProduct", "CartProduct can't be null");
        public static Error CartNotFoundForCustomer(Guid id) => new("CartNotFoundForCustomer", $"Cart was not found with this customer Id: {id}");
        public static Error ProductNotFoundInCart(Guid id) => new("ProductNotFound", $"Product was not found in the cart with this Id: {id}");
    }

    public static class Customer
    {
        public static Error CustomerNotFound(Guid id) => new("CustomerNotFound", $"Customer was not found with this Id: {id}");
    }

    public static class Product
    {
        public static Error CustomerNotFound(Guid id) => new("CustomerNotFound", $"Customer was not found with this Id: {id}");
        public static Error ProductNotFound(Guid id) => new("ProductNotFound", $"Product was not found with this Id: {id}");
    }

    public static class Purchase
    {
        public static Error CheckoutIssue(string issue) => new("CheckoutIssue", $"CheckoutIssue: {issue}");
    }
}
