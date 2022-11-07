using AutoMapper;
using eCommerce.Application.Products.Dtos;
using eCommerce.Application.Products.Repositories;
using eCommerce.Domain.Products;
using eCommerce.Domain.Products.Spectifications;
using System;
using System.Collections.Generic;

namespace eCommerce.Application.Products.Services;

public class ProductService //: IProductService
{
    //private readonly IProductsRepository _productRepository;
    //private readonly IMapper _mapper;
    //public ProductService(IProductsRepository productRepository, IMapper mapper)
    //{
    //    _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    //    _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    //}

    //public IReadOnlyList<Product> GetAllProducts()
    //{
    //    return _productRepository.GetProducts(AllProductsSpec.Create());
    //}

    //public Option<ProductDto> Get(Guid id)
    //{
    //    var option = _productRepository.GetProductById(id);

    //    return _mapper.Map<Option<Product>, Option<ProductDto>>(option);
    //}

    //public ProductDto Add(ProductDto dto)
    //{
    //    Product product = null;
            
    //        //Product.Create(dto.Name, dto.Quantity, dto.Cost, category.First());
    //    _productRepository.AddProduct(product);

    //    return _mapper.Map<Product, ProductDto>(product);
    //}

    //public void Update(ProductForUpdateDto product)
    //{
    //    var productForUpdate = _productRepository.GetProductById(product.Id);

    //    productForUpdate.Match(value =>
    //    {
    //        Product product = null;
    //        //Product.Create(value.Id, value.Name, value.Quantity, value.Cost, null)
    //        _productRepository.UpdateProduct(product);
    //    },
    //    () => throw new Exception("Product Is Not Found"));
    //}

    //public void Remove(Guid id)
    //{
    //    var productForDelete = _productRepository.GetProductById(id);

    //    productForDelete.Match(value =>
    //    {
    //        _productRepository.DeleteProduct(value);
    //    },
    //    () =>
    //    {
    //        throw new Exception("Product Is Not Found");
    //    });
    //}
}
