using System;

namespace DAL.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}