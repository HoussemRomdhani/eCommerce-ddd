using System.Linq.Expressions;

namespace eCommerce.Domain.SharedKernel.Specifications;

public abstract class SpecificationBase<T> : ISpecification<T>
{
    private Func<T, bool> _compiledExpression;

    private Func<T, bool> CompiledExpression => _compiledExpression ??= Criteria.Compile();

    public abstract Expression<Func<T, bool>> Criteria { get; }

    public bool IsSatisfiedBy(T obj)
    {
        return CompiledExpression(obj);
    }
}
