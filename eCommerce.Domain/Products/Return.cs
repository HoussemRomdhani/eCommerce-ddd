using eCommerce.Domain.Customers;
using System;

namespace eCommerce.Domain.Products
{
    public class Return
    {
        public Product Product { get; protected set; }
        public Customer Customer { get; protected set; }
        public ReturnReason Reason { get; protected set; }
        public DateTime Created { get; protected set; }
        public string Note { get; protected set; }
    }
}
