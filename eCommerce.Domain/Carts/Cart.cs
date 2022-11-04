using eCommerce.Domain.Core;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace eCommerce.Domain.Carts
{
    public class Cart : IEntity
    {
        private List<CartProduct> _cartProducts = new List<CartProduct>();
        public  Guid Id { get; protected set; }

        public ReadOnlyCollection<CartProduct> Products
        {
            get { return _cartProducts.AsReadOnly(); }
        }

        public Guid CustomerId { get; protected set; }

        public decimal TotalCost
        {
            get
            {
                return Products.Sum(cartProduct => cartProduct.Quantity * cartProduct.Cost);
            }
        }

        public decimal TotalTax
        {
            get
            {
                return Products.Sum(cartProducts => cartProducts.Tax);
            }
        }

        public static Cart Create(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var cart = new Cart
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id
            };

            // DomainEvents.Raise<CartCreated>(new CartCreated() { Cart = cart });

            return cart;
        }

        public void Add(CartProduct cartProduct)
        {
            if (cartProduct == null)
                throw new ArgumentNullException();

            //    DomainEvents.Raise<ProductAddedCart>(new ProductAddedCart() { CartProduct = cartProduct });

            _cartProducts.Add(cartProduct);
        }

        public void Remove(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("product");

            //CartProduct cartProduct =
            //   cartProducts.Find(new ProductInCartSpec(product).IsSatisfiedBy);

            // DomainEvents.Raise<ProductRemovedCart>(new ProductRemovedCart() { CartProduct = cartProduct });

            //  cartProducts.Remove(cartProduct);
        }

        public void Clear()
        {
            _cartProducts.Clear();
        }
    }
}
