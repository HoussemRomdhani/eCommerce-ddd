using System.Collections.Generic;
using System;
using eCommerce.Domain.Products;
using LanguageExt;
using eCommerce.Domain.Core;

namespace eCommerce.Application.Products.Repositories;

public interface ICategoryRepository
{
    void AddCategory(Category entity);
    IReadOnlyList<Category> GetCategories(ISpecification<Category> spec);
    Option<Category> GetCategoryById(Guid id);
    Option<Category> GetSingleCategory(ISpecification<Category> spec);
    void DeleteCategory(Category entity);
    void UpdateCategory(Category entity);
}
