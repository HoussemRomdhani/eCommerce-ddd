using eCommerce.Domain.Common;
using System.Collections.Generic;
using System;

namespace eCommerce.Domain.Products.Repositories
{
    public interface ICategoryRepository
    {
        void AddCategory(Category entity);
        IReadOnlyList<Category> FindCategories(ISpecification<Category> criteria);
        Category FindCategoryById(Guid id);
        Category FindSingleCategory(ISpecification<Category> criteria);
        void RemoveCategory(Category entity);
        void UpdateCategory(Category entity);
    }
}
