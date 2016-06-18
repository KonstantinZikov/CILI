using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM
{
    public class Role : IOrmEntity
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
