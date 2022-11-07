using eCommerce.Domain.Carts;
using System;

namespace eCommerce.Application.Carts.Dtos
{
    public class CheckoutResultDto
    {
        public Guid? PurchaseId { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalTax { get; set; }
     //   public CheckOutIssue? CheckOutIssue { get; set; }
    }
}