using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using Dal.Interfaces;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        public UnitOfWork(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context is null");
            Context = context;
        }

        public void Commit()
        {
            try
            {
                Context?.SaveChanges();
            }
            catch(Exception ex) when
            (   ex.GetType() == typeof(DbUpdateConcurrencyException)||
                ex.GetType() == typeof(DbUpdateException) ||
                ex.GetType() == typeof(DbEntityValidationException))
            {
                throw new UnitOfWorkException("Some db problems.", ex);
            }
        }

        public void Dispose()
            =>Context?.Dispose();
    }
}