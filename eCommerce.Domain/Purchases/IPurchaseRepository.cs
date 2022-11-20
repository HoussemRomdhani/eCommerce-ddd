using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Domain.Purchases;

public interface IPurchaseRepository
{
    Task AddPurchaseAsync(Purchase purchase, CancellationToken cancellationToken = default);
}
