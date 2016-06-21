using System;

namespace CilPlayground.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public DateTime RegistrationTime { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}