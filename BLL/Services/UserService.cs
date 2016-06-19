using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interfaces.Repository;
using DAL.Interfaces.DTO;
using System;
using BLL.Interface.Exceptions;
using System.Net.Mail;

namespace BLL.Services
{
    public class UserService : BaseService<UserEntity, DalUser> , IUserService
    {
        public UserService(IUnitOfWork unitOfWork, IRepository<DalUser> repository) 
            : base(unitOfWork,repository){ }

        protected override void Check(UserEntity entity)
        {
            if (entity.Id < 0)
                throw new ServiceException("Id must be greater then zero.");
            if (entity.Name.Length < 4)
                throw new ServiceException("Name must contain at least four characters.");
            if (entity.RegistrationTime < new DateTime(2016,06,01))
                throw new ServiceException("RegistrationTime must be greater then 2016.06.01");
            if (entity.RoleId < 0)
                throw new ServiceException("RoleId must be greater then zero.");

            try
            {
                new MailAddress(entity.Mail);
            }
            catch (FormatException)
            {
                throw new ServiceException("e-Mail doesn't match the format.");
            }

        }

        protected override UserEntity ToBll(DalUser user)
        {
            return new UserEntity
            {
                Id = user.Id,
                Mail = user.Mail,
                Name = user.Name,
                Password = user.Password,
                Salt = user.Salt,
                RegistrationTime = user.RegistrationTime,
                RoleId = user.RoleId
            };
        }

        protected override DalUser ToDal(UserEntity user)
        {
            return new DalUser
            {
                Id = user.Id,
                Mail = user.Mail,
                Name = user.Name,
                Password = user.Password,
                Salt = user.Salt,
                RegistrationTime = user.RegistrationTime,
                RoleId = user.RoleId
            };
        }
    }
}
