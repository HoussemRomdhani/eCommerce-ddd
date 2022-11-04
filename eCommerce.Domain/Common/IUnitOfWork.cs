using System;

namespace eCommerce.Domain.Common
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        void Rollback();
    }
}
