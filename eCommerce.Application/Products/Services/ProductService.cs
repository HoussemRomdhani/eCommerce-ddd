using AutoMapper;
using eCommerce.Application.Products.Dtos;
using eCommerce.Application.Products.Repositories;
using eCommerce.Domain.Products;
using eCommerce.Domain.Products.Spectifications;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Application.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductsRepository productRepository,
                              ICategoryRepository categoryRepository,
                              IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IReadOnlyList<Product> GetAllProducts()
        {
            return _productRepository.GetProducts(AllProductsSpec.Create());
        }

        public Option<ProductDto> Get(Guid id)
        {
            var option = _productRepository.GetProductById(id);

            return _mapper.Map<Option<Product>, Option<ProductDto>>(option);
        }

        public ProductDto Add(ProductDto dto)
        {
            var category = _categoryRepository.GetSingleCategory(CategoryNameContainsSpec.Create(dto.CategoryName));

            if (category.IsNone)
                throw new Exception("Category Is Not Found");

             var product = Product.Create(dto.Name, dto.Quantity, dto.Cost, category.First());
            _productRepository.AddProduct(product);

            return _mapper.Map<Product, ProductDto>(product);
        }

        public void Update(ProductForUpdateDto product)
        {
            var productForUpdate = _productRepository.GetProductById(product.Id);

            productForUpdate.Match(value =>
            {
                _productRepository.UpdateProduct(Product.Create(value.Id, value.Name, value.Quantity, value.Cost, null));
            },
            () => throw new Exception("Product Is Not Found"));
        }

        public void Remove(Guid id)
        {
            var productForDelete = _productRepository.GetProductById(id);

            productForDelete.Match(value =>
            {
                _productRepository.DeleteProduct(value);
            },
            () => throw new Exception("Product Is Not Found"));
        }
    }
}
