using System.Collections.Generic;
using System;

namespace eCommerce.Domain.Core;
public interface IRepository<TEntity>
    where TEntity : IAggregateRoot
{
    TEntity FindById(Guid id);
    TEntity FindOne(ISpecification<TEntity> spec);
    IReadOnlyList<TEntity> Find(ISpecification<TEntity> spec);
    void Add(TEntity entity);
    void Remove(TEntity entity);
    void Update(TEntity entity);
}
