using ecommerce.Apis.Common;
using eCommerce.Application.Products.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eCommerce.Apis.Products.Controllers;

[Route("products")]
public class ProductsController : ApiController
{
    public ProductsController(IMediator mediator ): base(mediator)
    {
    }

    [HttpGet]
    public IActionResult Get()
    {
        //var result = _productService.GetAllProducts();
        //return Ok(result);
        return Ok();
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Get(Guid id)
    {
        //var result = _productService.Get(id);

        //return result.Match<IActionResult>(Ok, NotFound);
        return Ok();

    }

    [HttpPost]
    public IActionResult Add([FromBody] ProductDto productDto)
    {
        //var response = _productService.Add(productDto);

        //return Ok(response);
        return Ok();
    }
}
