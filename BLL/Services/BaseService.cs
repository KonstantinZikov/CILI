using DAL.Interfaces.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Exceptions;
using BLL.Interface.Entities;
using Dal.Interfaces;

namespace BLL.Services
{
    public abstract class BaseService<BllType,DalType> 
        where DalType : IEntity
        where BllType : BllEntity
    {
        protected abstract BllType ToBll(DalType dal);
        protected abstract DalType ToDal(BllType bll);
        protected abstract void Check(BllType entity);

        protected readonly IUnitOfWork unitOfWork;
        protected readonly IRepository<DalType> repository;

        protected BaseService(IUnitOfWork unitOfWork, IRepository<DalType> repository)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
        }

        public virtual BllType Get(int id)
            => ToBll(repository.GetById(id));

        public virtual IEnumerable<BllType> GetAllEntities()
            => repository.GetAll().Select(ToBll);

        public virtual void Create(BllType entity)
        {
            Check(entity);
            try
            {
                repository.Create(ToDal(entity));
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public virtual void Delete(BllType entity)
        {
            if (entity.Id < 0)
                throw new ServiceException("Id must be greater then zero.");
            try
            {
                repository.Delete(ToDal(entity));
                unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }

        public virtual void Update(BllType entity)
        {
            Check(entity);
            try
            {
                repository.Update(ToDal(entity));
                unitOfWork.Commit();
            }
            catch(Exception ex)
            {
                throw new ServiceException(ex.Message, ex);
            }
        }
       
    }
}
