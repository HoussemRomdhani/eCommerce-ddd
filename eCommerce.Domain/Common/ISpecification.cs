using System;
using System.Linq.Expressions;

namespace eCommerce.Domain.Common
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        bool IsSatisfiedBy(T obj);
    }
}
