using eCommerce.Domain.Purchases;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure;

public class InMemoryPurchaseRepository : IPurchaseRepository
{
    private List<Purchase> _store = new() { };
    public Task AddPurchaseAsync(Purchase purchase, CancellationToken cancellationToken = default)
    {
        _store.Add(purchase);
        return Task.CompletedTask;
    }
}
