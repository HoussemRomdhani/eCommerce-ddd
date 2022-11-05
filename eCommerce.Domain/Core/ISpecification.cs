using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Core;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    bool IsSatisfiedBy(T obj);
}
