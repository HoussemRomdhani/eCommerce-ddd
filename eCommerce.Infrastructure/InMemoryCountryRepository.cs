using eCommerce.Domain.Countries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure;

public class InMemoryCountryRepository : ICountryRepository
{
    private readonly List<Country> _store = new()
        {
            Country.Create(Guid.Parse("8c99ac45-f3eb-4e1f-ab63-5ceb192a5dba"), "TUN", "Tunisia"),
            Country.Create(Guid.Parse("e17a906f-d786-49e2-897d-83fec6558cf9"), "USA" ,"USA"),
            Country.Create(Guid.Parse("27a6fb10-1c51-43b7-945a-7a1f8ef58c33"), "DEU", "Germany"),
            Country.Create(Guid.Parse("4f14a0df-b397-4ca3-83d5-d3b759a74de4"), "FRA" ,"France")
        };

    public Task<Country> GetCountryByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.FirstOrDefault(x => x.Code == code));
    }

    public Task<Country> GetCountryByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.FirstOrDefault(x => x.Id == id));
    }

    public Task<Country> GetCountryByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_store.FirstOrDefault(x => x.Name == name));
    }
}
