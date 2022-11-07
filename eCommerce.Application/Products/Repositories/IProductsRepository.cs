using eCommerce.Domain.Products;
using System;
using System.Collections.Generic;
using eCommerce.Domain.SharedKernel.Specifications;

namespace eCommerce.Application.Products.Repositories;

public interface IProductsRepository
{
    void AddProduct(Product entity);
    IReadOnlyList<Product> GetProducts(ISpecification<Product> spec);
    Product GetProductById(Guid id);
    Product GetSingleProduct(ISpecification<Product> spec);
    void DeleteProduct(Product entity);
    void UpdateProduct(Product entity);
}