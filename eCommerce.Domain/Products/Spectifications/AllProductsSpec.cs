using eCommerce.Domain.SharedKernel.Specifications;
using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Products.Spectifications;

public class AllProductsSpec : SpecificationBase<Product>
{
    public override Expression<Func<Product, bool>> Criteria => product => true;

    private AllProductsSpec()
    {
    }
    public static AllProductsSpec Create() => new AllProductsSpec();
}
