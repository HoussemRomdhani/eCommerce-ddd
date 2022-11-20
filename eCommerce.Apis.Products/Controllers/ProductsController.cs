using ecommerce.Apis.Contrats.Customers;
using eCommerce.Application.Customers.Dtos.Responses;
using eCommerce.Application.Products.Commands.CreateProduct;
using eCommerce.Application.Products.Commands.UpdateProduct;
using eCommerce.Application.Products.Dtos.Requests;
using eCommerce.Application.Products.Dtos.Responses;
using eCommerce.Application.Products.Queries.GetAllProducts;
using eCommerce.Application.Products.Queries.GetProductById;
using eCommerce.Domain.SharedKernel.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace eCommerce.Apis.Products.Controllers;

[Route("products")]
public class ProductsController : ApiController
{
    public ProductsController(IMediator mediator ): base(mediator)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerResponseDto>), 200)]
    [ProducesResponseType(typeof(Error), 500)]
    public async Task<IActionResult> GetAll()
    {
        return ToResult(await Mediator.Send(GetAllProductsQuery.Create()));
    }

    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(typeof(ProductResponseDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(Error), 500)]
    public async Task<IActionResult> GetById([Required] Guid id)
    {
        return ToResult(await Mediator.Send(GetProductByIdQuery.Create(id)));
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
    [ProducesResponseType(typeof(Error), 500)]
    public async Task<IActionResult> Add([FromBody] CreateProductRequestDto dto)
    {
        return ToResult(await Mediator.Send(CreateProductCommand.Create(dto.Name, dto.Quantity, dto.Cost, dto.ProductCode)));
    }

    [Route("{id}")]
    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
    [ProducesResponseType(typeof(Error), 500)]
    public async Task<IActionResult> Update([Required] Guid id, [FromBody] UpdateProductRequestDto dto)
    {
        return ToResult(await Mediator.Send(UpdateProductCommand.Create(id, dto.Name, dto.Quantity, dto.Cost, dto.ProductCode)));
    }
}
