using System;

namespace eCommerce.Domain.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
