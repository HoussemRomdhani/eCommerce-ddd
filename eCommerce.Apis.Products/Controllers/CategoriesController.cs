using eCommerce.Application.Products.Dtos;
using eCommerce.Application.Products.Services;
using eCommerce.Domain.Products;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eCommerce.Apis.Products.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_categoryService.GetAll());
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult Get(Guid id)
    {
        return _categoryService.Get(id).Match<IActionResult>(Ok, NotFound);
    }

    [HttpPost]
    public IActionResult Add([FromBody] CategoryDto categoryDto)
    {
        return Ok(_categoryService.Add(Category.Create(categoryDto.Name)));
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(Guid id, [FromBody] CategoryDto codeDto)
    {
        var result = _categoryService.Get(id).Match<IActionResult>(value =>
                             {
                                 _categoryService.Update(Category.Create(value.Id, codeDto.Name));
                                 return NoContent();
                             }, NotFound);

        return result;
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(Guid id)
    {
        var result = _categoryService.Get(id).Match<IActionResult>(value =>
                         {
                             _categoryService.Remove(value);
                             return NoContent();
                         }, NotFound);

        return result;
    }
}
