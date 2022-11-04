using System;
using eCommerce.Domain.Core;

namespace eCommerce.Domain.Products
{
    public class Product : IEntity
    {
     //   private List<Return> returns = new List<Return>();

        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime Modified { get; protected set; }
        public bool Active { get; protected set; }
        public int Quantity { get; protected set; }
        public decimal Cost { get; protected set; }
        public Category Category { get; protected set; }

        //public ReadOnlyCollection<Return> Returns
        //{
        //    get
        //    {
        //        return returns.AsReadOnly();
        //    }
        //}
        public static Product Create(string name, int quantity, decimal cost, Category category)
        {
            return Create(Guid.NewGuid(), name, quantity, cost, category);
        }
        public static Product Create(Guid id, string name, int quantity, decimal cost, Category category)
        {
            var product = new Product
            {
                Id = id,
                Name = name,
                Quantity = quantity,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Active = true,
                Cost = cost,
                Category = category
            };

            //  DomainEvents.Raise<ProductCreated>(new ProductCreated() { Product = product });

            return product;
        }
    }
}
