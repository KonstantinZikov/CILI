using DAL.Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Exceptions;
using BLL.Interface.Entities;
using Dal.Interfaces;

namespace BLL.Services
{
    public abstract class BaseService<TBllType,TDalType> 
        where TDalType : IEntity
        where TBllType : BllEntity
    {
        protected abstract TBllType ToBll(TDalType dal);
        protected abstract TDalType ToDal(TBllType bll);
        protected abstract void Validate(TBllType entity);

        protected readonly IUnitOfWork unitOfWork;
        protected readonly IRepository<TDalType> Repository;

        protected BaseService(IUnitOfWork unitOfWork, IRepository<TDalType> repository)
        {
            this.unitOfWork = unitOfWork;
            Repository = repository;
        }

        public virtual TBllType Get(int id)
            => ToBll(Repository.GetById(id));

        public virtual IEnumerable<TBllType> GetAllEntities()
            => Repository.GetAll().Select(ToBll);

        public virtual void Create(TBllType entity)
        {
            Validate(entity);
            try
            {
                Repository.Create(ToDal(entity));
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public virtual void Delete(TBllType entity)
        {
            if (entity.Id < 0)
                throw new ServiceException("Id must be greater then zero.");
            try
            {
                Repository.Delete(ToDal(entity));
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public virtual void Update(TBllType entity)
        {
            Validate(entity);
            try
            {
                Repository.Update(ToDal(entity));
                unitOfWork.Commit();
            }
            catch(Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }
       
    }
}
