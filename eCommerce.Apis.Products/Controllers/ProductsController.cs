using eCommerce.Application.Products.Dtos;
using eCommerce.Application.Products.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eCommerce.Apis.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _productService.GetAllProducts();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id)
        {
            var result = _productService.Get(id);

            return result.Match<IActionResult>(Ok, NotFound);
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProductDto productDto)
        {
            var response = _productService.Add(productDto);

            return Ok(response);
        }
    }
}
