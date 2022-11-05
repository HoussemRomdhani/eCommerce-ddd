using eCommerce.Domain.Carts;
using System;

namespace eCommerce.Application.Carts;

public class CheckOutResultDto
{
    public Guid? PurchaseId { get; set; }
    public decimal TotalCost { get; set; }
    public decimal TotalTax { get; set; }
    public CheckOutIssue? CheckOutIssue { get; set; }
}
