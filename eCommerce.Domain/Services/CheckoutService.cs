using eCommerce.Domain.Carts;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using eCommerce.Domain.Purchases;
using eCommerce.Domain.Products.Spectifications;
using System;
using eCommerce.Domain.Core;

namespace eCommerce.Domain.Services
{
    public class CheckoutService : IDomainService
    {
        private readonly IRepository<Purchase> _purchaseRepository;
        private readonly IRepository<Product> _productRepository;
        public CheckoutService(IRepository<Purchase> purchaseRepository, IRepository<Product> productRepository)
        {
            _purchaseRepository = purchaseRepository ?? throw new ArgumentNullException(nameof(purchaseRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        public PaymentStatus CustomerCanPay(Customer customer)
        {
            if (customer.Balance < 0)
                return PaymentStatus.UnpaidBalance;

            if (customer.GetCreditCardsAvailble().Count == 0)
                return PaymentStatus.NoActiveCreditCardAvailable;

            return PaymentStatus.OK;
        }

        public ProductState ProductCanBePurchased(Cart cart)
        {
            ISpecification<Product> faultyProductSpec = new ProductReturnReasonSpec(ReturnReason.Faulty);

            foreach (CartProduct cartProduct in cart.Products)
            {
                Product product = this._productRepository.FindById(cartProduct.ProductId);
                if (product == null)
                    throw new Exception($"Product {cartProduct.ProductId} not found");

                bool isInStock = new ProductIsInStockSpec(cartProduct).IsSatisfiedBy(product);

                if (!isInStock)
                    return ProductState.NotInStock;

                bool isFaulty = faultyProductSpec.IsSatisfiedBy(product);

                if (isFaulty)
                    return ProductState.IsFaulty;
            }
            return ProductState.OK;
        }

        public CheckOutIssue? CanCheckOut(Customer customer, Cart cart)
        {
            var paymentStatus = CustomerCanPay(customer);

            if (paymentStatus != PaymentStatus.OK)
                return (CheckOutIssue)paymentStatus;

            var productState = ProductCanBePurchased(cart);

            if (productState != ProductState.OK)
                return (CheckOutIssue)productState;

            return null;
        }

        public Purchase Checkout(Customer customer, Cart cart)
        {
            CheckOutIssue? checkoutIssue = CanCheckOut(customer, cart);
            if (checkoutIssue.HasValue)
                throw new Exception(checkoutIssue.Value.ToString());

            Purchase purchase = Purchase.Create(cart);

            _purchaseRepository.Add(purchase);

            cart.Clear();

            //   DomainEvents.Raise(new CustomerCheckedOut() { Purchase = purchase });

            return purchase;
        }

    }
}
