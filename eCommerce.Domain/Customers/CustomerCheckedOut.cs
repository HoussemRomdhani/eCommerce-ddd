using eCommerce.Domain.Core;
using eCommerce.Domain.Purchases;

namespace eCommerce.Domain.Customers;

public class CustomerCheckedOut : DomainEvent
{
    public Purchase Purchase { get; set; }

    public override void Flatten()
    {
        Args.Add("CustomerId", Purchase.CustomerId);
        Args.Add("PurchaseId", Purchase.Id);
        Args.Add("TotalCost", Purchase.TotalCost);
        Args.Add("TotalTax", Purchase.TotalTax);
        Args.Add("NumberOfProducts", Purchase.Products.Count);
    }
}
