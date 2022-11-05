using eCommerce.Domain.Products;
using LanguageExt;
using System;
using System.Collections.Generic;

namespace eCommerce.Application.Products.Services;

public interface ICategoryService
{
    IReadOnlyList<Category> GetAll();
    Option<Category> Get(Guid id);
    Category Add(Category code);
    void Update(Category code);
    void Remove(Category code);
}
