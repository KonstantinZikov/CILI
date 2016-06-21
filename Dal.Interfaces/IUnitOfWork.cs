using System;

namespace Dal.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}