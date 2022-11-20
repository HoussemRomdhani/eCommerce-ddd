using System.Threading.Tasks;
using System.Threading;
using System;

namespace eCommerce.Domain.Countries;

public interface ICountryRepository
{
    Task<Country> GetCountryByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Country> GetCountryByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<Country> GetCountryByNameAsync(string name, CancellationToken cancellationToken = default);
}
