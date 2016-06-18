using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM
{
    public class User : IOrmEntity
    {
        public int Id { get; set; }

        [Required]
        public string Mail { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public DateTime RegistrationTime { get; set; }

        public int RoleId { get; set; }
    }
}