using eCommerce.Domain.Carts;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using eCommerce.Domain.Purchases;
using eCommerce.Domain.Products.Spectifications;
using System;
using eCommerce.Domain.SharedKernel;
using eCommerce.Domain.SharedKernel.Repositories;
using System.Threading.Tasks;
using eCommerce.Domain.SharedKernel.Results;
using eCommerce.Domain.Common.DomainErrors;

namespace eCommerce.Domain.Services;

public class CheckoutService : IDomainService
{
    private readonly IRepositoryBase<Purchase> _purchaseRepository;
    private readonly IReadRepositoryBase<Product> _productRepository;
    public CheckoutService(IRepositoryBase<Purchase> purchaseRepository, IReadRepositoryBase<Product> productRepository)
    {
        _purchaseRepository = purchaseRepository ?? throw new ArgumentNullException(nameof(purchaseRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public static PaymentStatus CustomerCanPay(Customer customer)
    {
        if (customer.Balance < 0)
            return PaymentStatus.UnpaidBalance;

        if (customer.GetCreditCardsAvailble().Count == 0)
            return PaymentStatus.NoActiveCreditCardAvailable;

        return PaymentStatus.OK;
    }

    public async Task<Result<ProductState>> ProductCanBePurchasedAsync(Cart cart)
    {
        var faultyProductSpec = new ProductReturnReasonSpec(ReturnReason.Faulty);

        foreach (CartProduct cartProduct in cart.Products)
        {
            var product = await _productRepository.GetByIdAsync(cartProduct.ProductId);

            if (product is null)
                return Result.Failure<ProductState>(DomainErrors.Product.ProductNotFound(cartProduct.ProductId));

            bool isInStock = new ProductIsInStockSpec(cartProduct).IsSatisfiedBy(product);

            if (!isInStock)
                return ProductState.NotInStock;

            bool isFaulty = faultyProductSpec.IsSatisfiedBy(product);

            if (isFaulty)
                return ProductState.IsFaulty;
        }

        return ProductState.OK;
    }

    public async Task<Result<CheckOutIssue?>> CanCheckoutAsync(Customer customer, Cart cart)
    {
        var paymentStatus = CustomerCanPay(customer);

        if (paymentStatus != PaymentStatus.OK)
            return (CheckOutIssue)paymentStatus;

        var productStateResult = await ProductCanBePurchasedAsync(cart);

        if (productStateResult.IsFailure)
            return Result.Failure<CheckOutIssue?>(productStateResult.Error);

        var value = productStateResult.Value;

        if (value != ProductState.OK)
            return (CheckOutIssue)value;

        return null;
    }

    public Purchase Checkout(Cart cart)
    {
        var result = Purchase.Create(cart);

        _purchaseRepository.AddAsync(result);

        cart.Clear();

        _purchaseRepository.SaveChangesAsync();

        return result;
    }

}
