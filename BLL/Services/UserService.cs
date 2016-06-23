using BLL.Interface.Entities;
using BLL.Interface.Services;
using DAL.Interfaces.DTO;
using System;
using BLL.Interface.Exceptions;
using System.Net.Mail;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Dal.Interfaces;

namespace BLL.Services
{
    public class UserService : BaseService<UserEntity, DalUser> , IUserService
    {
        private const int SALT_SIZE = 64;

        private readonly IRepository<DalRole> _roleRepository;

        public UserService(IUnitOfWork unitOfWork, 
            IRepository<DalUser> repository,IRepository<DalRole> roleRepository)
            : base(unitOfWork, repository)
        {
            _roleRepository = roleRepository;
        }

        protected override void Validate(UserEntity entity)
        {
            if (entity.Id < 0)
                throw new ServiceException("Id must be greater then zero.");

            if (entity.Name == null)
                throw new ServiceException("Name is null.");
            if (entity.Name.Length < 4)
                throw new ValidationException("Name must contain at least four symbols.");
            if (entity.Name.Length > 16)
                throw new ValidationException("Name's max length is 16 symbols");

            if (entity.RegistrationTime < new DateTime(2016,06,01))
                throw new ValidationException("RegistrationTime must be greater then 2016.06.01. Check data format.");
            if (entity.RoleId < 0)
                throw new ServiceException("RoleId must be greater then zero.");

            if (entity.Mail == null)
                throw new ServiceException("Mail is null.");
            if (entity.Mail.Length > 64)
                throw new ValidationException("Email's max length is 64 symbols");
            try
            {
                new MailAddress(entity.Mail);
            }
            catch (FormatException)
            {
                throw new ValidationException("e-Mail doesn't match the format.");
            }
            

        }

        protected override UserEntity ToBll(DalUser user)
        {
            if (user == null) return null;

            return new UserEntity
            {
                Id = user.Id,
                Mail = user.Mail,
                Name = user.Name,
                RegistrationTime = user.RegistrationTime,
                RoleId = user.RoleId
            };
        }

        protected override DalUser ToDal(UserEntity user)
        {
            if (user == null) return null;

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

        public override void Update(UserEntity entity)
        {
            
            //Last admin can't be updated.
            var adminRole = _roleRepository.GetByPredicate(r => r.Name == "Admin").FirstOrDefault();
            if (entity?.RoleId == adminRole?.Id)
            {
                var adminsCount = Repository.GetByPredicate(u => u.RoleId == adminRole.Id).Count();
                if (adminsCount == 1)
                    throw new ValidationException("can't update last admin. Please, create" +
                                            " a one more admin profile before updating.");
            }
               
            if (entity?.Salt != null)
                throw new ServiceException("Salt can't be changed.");
            if (!string.IsNullOrEmpty(entity?.Password))
            { 
                var cryptoProvider = new RNGCryptoServiceProvider();
                var saltBytes = new byte[SALT_SIZE];
                cryptoProvider.GetBytes(saltBytes);
                entity.Salt = Encoding.Default.GetString(saltBytes);
                entity.Password = HashPassword(entity.Password, entity.Salt);

                if (NameMatches(entity) > 1)
                    throw new ValidationException($"User with name {entity.Name} already exist.");
                if (MailMatches(entity) > 1)
                    throw new ValidationException($"User with e-mail {entity.Mail} already exist.");
            }

            base.Update(entity);
        }

        public override void Delete(UserEntity entity)
        {
            //Last admin can't be deleted.
            var adminRole = _roleRepository.GetByPredicate(r => r.Name == "Admin").FirstOrDefault();
            if (entity?.RoleId == adminRole?.Id)
            {
                var adminsCount = Repository.GetByPredicate(u => u.RoleId == adminRole.Id).Count();
                if (adminsCount == 1)
                    throw new ValidationException("can't delete last admin. Please, create" +
                                            " a one more admin profile before deleting.");
            }

            base.Delete(entity);
        }

        public override void Create(UserEntity entity)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            var saltBytes = new byte[SALT_SIZE];
            cryptoProvider.GetBytes(saltBytes);
            entity.Salt = Encoding.Default.GetString(saltBytes);
            entity.Password = HashPassword(entity.Password, entity.Salt);

            if (NameMatches(entity) != 0)
                throw new ValidationException($"User with name {entity.Name} already exist.");
            if (MailMatches(entity) != 0)
                throw new ValidationException($"User with e-mail {entity.Mail} already exist.");

            base.Create(entity);
        }

        public bool CheckPassword(string name, string password)
        {
            var user = Repository.GetByPredicate(u => u.Name == name).FirstOrDefault();
            if (user == null) return false;
            if (HashPassword(password, user.Salt) == user.Password)
                return true;
            return false;
        }

        private static string HashPassword(string password, string salt)
        {
            var fullPassword = password + salt;
            var fullPasswordBytes = Encoding.Default.GetBytes(fullPassword);
            using (SHA512 sha = new SHA512Managed())
            {
                return Encoding.Default.GetString(sha.ComputeHash(fullPasswordBytes));
            }
        }

        private int NameMatches(UserEntity entity)
            => Repository.GetAll().Count(u => u.Name.Equals(entity.Name,
                StringComparison.InvariantCultureIgnoreCase));


        private int MailMatches(UserEntity entity)
            => Repository.GetAll().Count(u => u.Mail.Equals(entity.Mail, 
                StringComparison.InvariantCultureIgnoreCase));

        public UserEntity Get(string name)
           => ToBll(Repository.GetByPredicate(u=>u.Name == name).FirstOrDefault());
    }
}
