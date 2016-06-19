using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        UserEntity Get(int id);
        IEnumerable<UserEntity> GetAllEntities();
        void Create(UserEntity instruction);
        void Update(UserEntity instruction);
        void Delete(UserEntity instruction);
    }
}