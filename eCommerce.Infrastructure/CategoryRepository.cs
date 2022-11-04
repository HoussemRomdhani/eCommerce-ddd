using eCommerce.Application.Products.Repositories;
using eCommerce.Domain.Common;
using eCommerce.Domain.Products;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Infrastructure
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogContext _catalogDbContext;

        public CategoryRepository(CatalogContext productsDbContext)
        {
            _catalogDbContext = productsDbContext ?? throw new ArgumentNullException(nameof(productsDbContext));
        }

        public void AddCategory(Category entity)
        {
            _catalogDbContext.Categories.Add(entity);
            _catalogDbContext.SaveChanges();
        }

        public IReadOnlyList<Category> GetCategories(ISpecification<Category> spec)
        {
            return _catalogDbContext.Categories.Where(spec.IsSatisfiedBy)
                                                .ToList();
        }

        public Option<Category> GetCategoryById(Guid id)
        {
            return _catalogDbContext.Categories.Find(id);
        }

        public Option<Category> GetSingleCategory(ISpecification<Category> spec)
        {
            return _catalogDbContext.Categories.SingleOrDefault(spec.IsSatisfiedBy);
        }

        public void DeleteCategory(Category entity)
        {
            _catalogDbContext.Categories.Remove(entity);
            _catalogDbContext.SaveChanges();
        }

        public void UpdateCategory(Category entity)
        {
            _catalogDbContext.Categories.Update(entity);
            _catalogDbContext.SaveChanges();
        }
    }
}
