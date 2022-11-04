using eCommerce.Application.Products.Repositories;
using eCommerce.Domain.Common;
using eCommerce.Domain.Products;
using eCommerce.Domain.Products.Spectifications;
using LanguageExt;
using System;
using System.Collections.Generic;

namespace eCommerce.Application.Products.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public Category Add(Category code)
        {
            _categoryRepository.AddCategory(code);
            return code;
        }

        public IReadOnlyList<Category> GetAll()
        {
            ISpecification<Category> specification = new DefaultCategorySpec();

            var result = _categoryRepository.GetCategories(specification);

            if (result == null)
                return new List<Category>();

            return result;
        }

        public Option<Category> Get(Guid id)
        {
            return _categoryRepository.GetCategoryById(id);
        }

        public void Remove(Category code)
        {
            _categoryRepository.DeleteCategory(code);
        }

        public void Update(Category code)
        {
            _categoryRepository.UpdateCategory(code);
        }
    }
}
