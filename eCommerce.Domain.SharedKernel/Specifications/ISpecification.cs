using System.Linq.Expressions;

namespace eCommerce.Domain.SharedKernel.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    bool IsSatisfiedBy(T obj);
}