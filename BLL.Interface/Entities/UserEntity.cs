using System;

namespace BLL.Interface.Entities
{
    public class UserEntity : BllEntity
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime RegistrationTime { get; set; }
        public int RoleId { get; set; }
    }
}