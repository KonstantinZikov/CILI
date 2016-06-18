using System.Data.Entity;
using DAL.Interfaces.DTO;
using DAL.Interfaces.Repository;
using ORM;

namespace DAL.Concrete
{
    public class UserRepository : BaseRepositpry<DalUser, User>
    {
        public UserRepository(DbContext context) : base(context) { }

        protected override User ToOrm(DalUser user)
        {
            return new User
            {
                Id = user.Id,
                Mail = user.Mail,
                Name = user.Name,
                RoleId = user.RoleId
            };
        }

        protected override void Update(DalUser dal, User user)
        {
            user.Mail = dal.Mail;
            user.Name = dal.Name;
            user.Password = dal.Password;
            user.RegistrationTime = dal.RegistrationTime;
            user.RoleId = dal.RoleId;
        }
    }
}