using System.Collections.Generic;

namespace eCommerce.Domain.Core;

public interface IDomainEventRepository
{
    void Add<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent;
    IEnumerable<DomainEventRecord> FindAll();
}
