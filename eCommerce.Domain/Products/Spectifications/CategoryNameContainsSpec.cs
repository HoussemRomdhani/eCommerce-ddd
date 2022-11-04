using eCommerce.Domain.Common;
using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Products.Spectifications
{
    public class CategoryNameContainsSpec : SpecificationBase<Category>
    {
        private readonly string _categoryName;
        private CategoryNameContainsSpec(string categoryName)
        {
            _categoryName = categoryName;
        }

        public static ISpecification<Category> Create(string categoryName)
        {
            return new CategoryNameContainsSpec(categoryName);
        }

        public override Expression<Func<Category, bool>> Criteria
        {
            get
            {
                return category => category.Name.TrimEnd() == _categoryName;
            }
        }
    }
}
