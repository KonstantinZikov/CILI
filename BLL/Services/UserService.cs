using System.Collections.Generic;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interfaces.Repository;
using DAL.Interfaces.DTO;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IRepository<DalUser> userRepository;

        public UserService(IUnitOfWork uow, IRepository<DalUser> repository)
        {
            this.uow = uow;
            userRepository = repository;
        }

        public UserEntity GetUserEntity(int id)
        {
            return userRepository.GetById(id).ToBllUser();
        }
        
        public IEnumerable<UserEntity> GetAllUserEntities()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }

        public void CreateUser(UserEntity user)
        {
            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }

        public void DeleteUser(UserEntity user)
        {
            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public void Update(UserEntity user)
        {
            userRepository.Update(user.ToDalUser());
            uow.Commit();
        }
    }
}
