using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interfaces.DTO;
using BLL.Interface.Exceptions;
using System.Linq;
using Dal.Interfaces;

namespace BLL.Services
{
    public class RoleService : BaseService<RoleEntity, DalRole>, IRoleService
    {
        public RoleService(IUnitOfWork unitOfWork, IRepository<DalRole> repository)
            : base(unitOfWork, repository){}

        protected override RoleEntity ToBll(DalRole dal)
        {
            if (dal == null) return null;

            return new RoleEntity()
            {
                Id = dal.Id,
                Name = dal.Name
            };
        }

        protected override DalRole ToDal(RoleEntity bll)
        {
            if (bll == null) return null;

            return new DalRole()
            {
                Id = bll.Id,
                Name = bll.Name        
            };
        }

        protected override void Validate(RoleEntity entity)
        {
            if (entity.Id < 0)
                throw new ServiceException(
                    $"Role id must be greater then zero, but it is {entity.Id}");
            if (entity.Name == null)
                throw new ServiceException("Role name is null.");
        }

        public RoleEntity Get(string name)
            => ToBll(Repository.GetByPredicate(r => r.Name == name).FirstOrDefault());
    }
}
