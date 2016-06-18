using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using ORM;
using static DAL.ExpressionParameterReplacer.ParameterReplacer;
using System.Collections.Generic;

namespace DAL.Concrete
{
    public abstract class BaseRepositpry<DalType,OrmType> : IRepository<DalType> 
        where DalType : IEntity 
        where OrmType : class, IOrmEntity
    {
        protected Expression<Func<OrmType, DalType>> ToDal;

        private Func<OrmType, DalType> _toDalCompiled;
        private Func<OrmType, DalType> ToDalCompiled
        {
            get
            {
                if (_toDalCompiled == null)
                    _toDalCompiled = ToDal.Compile();
                return _toDalCompiled;
            }
        }


        protected abstract OrmType ToOrm(DalType entity);
        protected abstract void Update(DalType dal, OrmType orm);

        private readonly DbContext context;

        public BaseRepositpry(DbContext context)
        {
            this.context = context;
        }

        public void Create(DalType entity)
            => context.Set<OrmType>().Add(ToOrm(entity));

        public void Delete(DalType entity)
        {
            var orm = ToOrm(entity);
            context.Set<OrmType>().Attach(orm);
            context.Set<OrmType>().Remove(orm);
        }

        public IQueryable<DalType> GetAll()
        { 
            return context.Set<OrmType>().Select(ToDal);
        }

        public DalType GetById(int key)
        {
            var orm = context.Set<OrmType>().FirstOrDefault(o => o.Id == key);
            return ToDalCompiled(orm);
        }

        public IQueryable<DalType> GetByPredicate(Expression<Func<DalType, bool>> function)
        {
            var newExpr = Replace<Func<DalType, bool>, Func<OrmType, bool>>
                (function, function.Parameters.First(), typeof(OrmType));
            return context.Set<OrmType>().Where(newExpr).Select(ToDal);
        }

        public void Update(DalType entity)
        {
            var orm = context.Set<OrmType>().Single(u => u.Id == entity.Id);
            Update(entity, orm);
        }
    }
}
