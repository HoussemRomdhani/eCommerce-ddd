﻿using System;

namespace eCommerce.Application.Customers;

public class CustomerPurchaseHistoryDto
{
    public Guid CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int TotalPurchases { get; set; }
    public int TotalProductsPurchased { get; set; }
    public decimal TotalCost { get; set; }
}