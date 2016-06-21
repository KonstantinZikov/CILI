using DAL.Interfaces.DTO;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using ORM;
using System.Collections.Generic;
using Dal.Interfaces;
using static DAL.ExpressionParameterReplacer.ParameterReplacer;

namespace DAL.Concrete
{
    public abstract class BaseRepositpry<TDalType,TOrmType> : IRepository<TDalType> 
        where TDalType : IEntity 
        where TOrmType : class, IOrmEntity
    {
        protected Expression<Func<TOrmType, TDalType>> ToDal;

        private Func<TOrmType, TDalType> _toDalCompiled;
        private Func<TOrmType, TDalType> ToDalCompiled
        {
            get
            {
                if (_toDalCompiled == null)
                {
                    _toDalCompiled = ToDal.Compile();
                }
                return _toDalCompiled;
            }
        }

        protected abstract TOrmType ToOrm(TDalType entity);
        protected abstract void Update(TDalType dal, TOrmType orm);

        private readonly DbContext _context;

        protected BaseRepositpry(DbContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context is null");
            _context = context;
        }

        public void Create(TDalType entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity is null");
            _context.Set<TOrmType>().Add(ToOrm(entity));
        }

        public void Delete(TDalType entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity is null");
            var orm = ToOrm(entity);
            _context.Set<TOrmType>().Attach(orm);
            _context.Set<TOrmType>().Remove(orm);
        }

        public IEnumerable<TDalType> GetAll()
        { 
            return _context.Set<TOrmType>().Select(ToDal);
        }

        public TDalType GetById(int id)
        {
            if (id <= 0)
                throw new InvalidOperationException("id can't be negative.");
            var orm = _context.Set<TOrmType>().FirstOrDefault(o => o.Id == id);
            return ToDalCompiled(orm);
        }

        public IQueryable<TDalType> GetByPredicate(Expression<Func<TDalType, bool>> function)
        {
            if (function == null)
                throw new ArgumentNullException("function is null");
            var newExpr = Replace<TDalType, TOrmType>(function, typeof(TOrmType));
            return _context.Set<TOrmType>().Where(newExpr).Select(ToDal);
        }

        public void Update(TDalType entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity is null");
            var orm = _context.Set<TOrmType>().Single(u => u.Id == entity.Id);
            Update(entity, orm);
        }
    }
}
