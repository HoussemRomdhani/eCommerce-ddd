using eCommerce.Domain.Carts;
using eCommerce.Domain.Customers;
using eCommerce.Domain.Products;
using eCommerce.Domain.Purchases;
using System;
using eCommerce.Domain.SharedKernel;
using System.Threading.Tasks;
using eCommerce.Domain.SharedKernel.Results;
using eCommerce.Domain.Common.DomainErrors;
using eCommerce.Domain.Products.Specifications;

namespace eCommerce.Domain.Services;

public class CheckoutService : IDomainService
{
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IProductRepository _productRepository;
    public CheckoutService(IPurchaseRepository purchaseRepository, IProductRepository productRepository)
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
            var product = await _productRepository.GetProductByIdAsync(cartProduct.ProductId);

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

    public async Task<Purchase> Checkout(Cart cart)
    {
        var result = Purchase.Create(cart);

       await _purchaseRepository.AddPurchaseAsync(result);

        cart.Clear();

       // _purchaseRepository.SaveChangesAsync();

        return result;
    }

}
