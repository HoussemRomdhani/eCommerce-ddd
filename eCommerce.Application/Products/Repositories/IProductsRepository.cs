using eCommerce.Domain.Core;
using eCommerce.Domain.Products;
using LanguageExt;
using System;
using System.Collections.Generic;

namespace eCommerce.Application.Products.Repositories
{
    public interface IProductsRepository
    {
        void AddProduct(Product entity);
        IReadOnlyList<Product> GetProducts(ISpecification<Product> spec);
        Option<Product> GetProductById(Guid id);
        Option<Product> GetSingleProduct(ISpecification<Product> spec);
        void DeleteProduct(Product entity);
        void UpdateProduct(Product entity);
    }
}