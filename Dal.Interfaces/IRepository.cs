using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interfaces.DTO;
using System.Data.Entity;
using System.Linq;

namespace DAL.Interfaces.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IQueryable<TEntity> GetAll();
        TEntity GetById(int key);
        IQueryable<TEntity> GetByPredicate(Expression<Func<TEntity, bool>> function);
        void Create(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}