using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IRoleService
    {
        RoleEntity Get(int id);
        RoleEntity Get(string name);
        IEnumerable<RoleEntity> GetAllEntities();
        void Create(RoleEntity instruction);
        void Update(RoleEntity instruction);
        void Delete(RoleEntity instruction);        
    }
}