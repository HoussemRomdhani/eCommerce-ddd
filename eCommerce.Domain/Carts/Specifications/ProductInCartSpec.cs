using eCommerce.Domain.Products;
using eCommerce.Domain.SharedKernel.Specifications;
using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Carts.Specifications;

public class ProductInCartSpec : SpecificationBase<CartProduct>
{
    private readonly Product product;

    public ProductInCartSpec(Product product)
    {
        this.product = product;
    }

    public override Expression<Func<CartProduct, bool>> Criteria
    {
        get
        {
            return cartProduct => cartProduct.ProductId == product.Id;
        }
    }
}