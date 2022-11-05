using System;

namespace eCommerce.Domain.Core;

public interface IAggregateRoot
{
    Guid Id { get; }
}
