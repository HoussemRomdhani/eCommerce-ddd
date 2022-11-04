using eCommerce.Domain.Core;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eCommerce.Infrastructure
{
    public abstract class BaseCatalogRepository
    {
        protected readonly CatalogContext _context;
        protected BaseCatalogRepository(CatalogContext productsDbContext)
        {
            _context = productsDbContext ?? throw new ArgumentNullException(nameof(productsDbContext));
        }

        protected virtual void Add<T>(T entity)  where T : class, IEntity
        {
            _context.Set<T>().Add(entity);
        }

        protected virtual IReadOnlyList<T> Find<T>(ISpecification<T> spec) 
            where T : class, IEntity
        {
            return _context.Set<T>().Where(spec.IsSatisfiedBy)
                                                .ToList();
        }

        protected virtual T FindById<T>(Guid id)
             where T : class, IEntity
        {
            return _context.Set<T>().Find(id);
        }

        protected virtual Option<T> FindOne<T>(ISpecification<T> spec)
             where T : class, IEntity
        {
             return _context.Set<T>().Find(spec.IsSatisfiedBy);
        }

        protected virtual void Delete<T>(T entity)
             where T : class, IEntity
        {
            _context.Set<T>().Remove(entity);
            SaveChanges();
        }

        protected virtual void Update<T>(T entity)
               where T : class, IEntity
        {
            var entry = _context.Set<T>().First(e => e.Id == entity.Id);
            _context.Entry(entry).CurrentValues.SetValues(entity);
            SaveChanges();
        }

        protected void SaveChanges() => _context.SaveChanges();
    }
}
