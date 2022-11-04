using System.Collections.Generic;
using System;
using eCommerce.Domain.Products;

namespace eCommerce.Domain.Common
{
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        TEntity FindById(Guid id);
        TEntity FindOne(ISpecification<TEntity> spec);
        IReadOnlyList<TEntity> Find(ISpecification<TEntity> spec);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}
