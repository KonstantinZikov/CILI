using System.Data.Entity;
using DAL.Interfaces.DTO;
using ORM;

namespace DAL.Concrete
{
    public class UserRepository : BaseRepositpry<DalUser, User>
    {
        public UserRepository(DbContext context) : base(context)
        {
            ToDal = (user) => new DalUser
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

        protected override User ToOrm(DalUser user)
        {
            return new User
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

        protected override void Update(DalUser dal, User user)
        {
            user.Mail = dal.Mail;
            user.Name = dal.Name;
            if (dal.Password != null)
                user.Password = dal.Password;
            if (dal.Salt != null)
                user.Salt = dal.Salt;
            user.RegistrationTime = dal.RegistrationTime;
            user.RoleId = dal.RoleId;
        }
    }
}