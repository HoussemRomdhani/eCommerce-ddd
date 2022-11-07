namespace eCommerce.Domain.SharedKernel.Repositories;

public interface IRepositoryBase<T> : IReadRepositoryBase<T> where T : EntityBase
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
