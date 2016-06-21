using System.Data.Entity;
using DAL.Interfaces.DTO;
using ORM;

namespace DAL.Concrete
{
    public class RoleRepository : BaseRepositpry<DalRole, Role>
    {
        public RoleRepository(DbContext context) : base(context)
        {
            ToDal = (role) => new DalRole
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        protected override Role ToOrm(DalRole role)
        {
            return new Role
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        protected override void Update(DalRole dal, Role role)
        {
            role.Name = dal.Name;
        }
    }
}