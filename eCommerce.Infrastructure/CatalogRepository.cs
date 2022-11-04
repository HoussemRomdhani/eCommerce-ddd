using eCommerce.Application.Products.Repositories;
using eCommerce.Domain.Core;
using eCommerce.Domain.Products;
using LanguageExt;
using System;
using System.Collections.Generic;

namespace eCommerce.Infrastructure
{
    public class CatalogRepository : BaseCatalogRepository, IProductsRepository, ICategoryRepository
    {
        public CatalogRepository(CatalogContext productsDbContext)
            : base(productsDbContext)
        {
        }

        public void AddProduct(Product entity)
        {
            Add(entity);
            SaveChanges();
        }
        public IReadOnlyList<Product> GetProducts(ISpecification<Product> spec) => Find(spec);
        public Option<Product> GetProductById(Guid id) => FindById<Product>(id);
        public Option<Product> GetSingleProduct(ISpecification<Product> spec)
        {
            return FindOne(spec);
        }

        public void DeleteProduct(Product entity) => Delete(entity);

        public void UpdateProduct(Product entity)
        {
            Update(entity);
            SaveChanges();
        }
        public void AddCategory(Category entity)
        {
            Add(entity);
            SaveChanges();
        }
        public IReadOnlyList<Category> GetCategories(ISpecification<Category> spec) => Find(spec);
        public Option<Category> GetCategoryById(Guid id) => FindById<Category>(id);
        public Option<Category> GetSingleCategory(ISpecification<Category> spec) => FindOne(spec);
        public void DeleteCategory(Category entity) => Delete(entity);
        public void UpdateCategory(Category entity)
        {
            Update(entity);
            SaveChanges();
        }
    }
}
