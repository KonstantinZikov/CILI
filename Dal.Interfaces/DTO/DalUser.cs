using System;
using System.Collections.Generic;

namespace DAL.Interfaces.DTO
{
    public class DalUser : IEntity
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationTime { get; set; }
        public int RoleId { get; set; }
    }
}