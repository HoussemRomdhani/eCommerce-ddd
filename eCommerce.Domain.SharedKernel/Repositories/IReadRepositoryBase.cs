using eCommerce.Domain.SharedKernel.Specifications;

namespace eCommerce.Domain.SharedKernel.Repositories;

public interface IReadRepositoryBase<T> where T : EntityBase
{
     Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
     Task<T> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
}
