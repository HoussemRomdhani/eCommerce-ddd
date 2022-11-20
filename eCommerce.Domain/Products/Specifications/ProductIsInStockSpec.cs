using eCommerce.Domain.Carts;
using eCommerce.Domain.SharedKernel.Specifications;
using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Products.Specifications;

public class ProductIsInStockSpec : SpecificationBase<Product>
{
    readonly CartProduct productCart;

    public ProductIsInStockSpec(CartProduct productCart)
    {
        this.productCart = productCart;
    }

    public override Expression<Func<Product, bool>> Criteria
    {
        get
        {
            return product => product.Id == productCart.ProductId &&
                              product.Active &&
                              product.Quantity >= productCart.Quantity;
        }
    }
}
