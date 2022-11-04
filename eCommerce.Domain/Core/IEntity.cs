using System;

namespace eCommerce.Domain.Core
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}
