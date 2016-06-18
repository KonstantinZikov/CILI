using BLL.Interface.Entities;
using DAL.Interfaces.DTO;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                RoleId = userEntity.RoleId
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity()
            {
                Id = dalUser.Id,
                Name = dalUser.Name,
                RoleId = dalUser.RoleId
            };
        }
    }
}
