using eCommerce.Application.Products.Dtos;
using eCommerce.Application.Products.Services;
using eCommerce.Domain.Products;
using Microsoft.AspNetCore.Mvc;
using System;

namespace eCommerce.Apis.Products.Controllers
{
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
        public IActionResult Get() => Ok(_categoryService.GetAll());

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(Guid id) 
                             => _categoryService.Get(id).Match<IActionResult>(Ok, NotFound);

        [HttpPost]
        public IActionResult Add([FromBody] CategoryDto categoryDto)
                             => Ok(_categoryService.Add(Category.Create(categoryDto.Name)));

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(Guid id, [FromBody] CategoryDto codeDto)
                             => _categoryService.Get(id).Match<IActionResult>(value =>
                             {
                                 _categoryService.Update(Category.Create(value.Id, codeDto.Name));
                                 return NoContent();
                             },
                                 NotFound);

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(Guid id)
                             => _categoryService.Get(id).Match<IActionResult>(value =>
                             {
                                 _categoryService.Remove(value);
                                 return NoContent();

                             },
                                 NotFound);
    }
}
