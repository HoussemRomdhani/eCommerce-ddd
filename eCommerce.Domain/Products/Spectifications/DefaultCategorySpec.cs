using eCommerce.Domain.Common;
using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Products.Spectifications
{
    public class DefaultCategorySpec : SpecificationBase<Category>
    {
        public override Expression<Func<Category, bool>> Criteria => category => true;
    }
}
