using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ORM
{
    public class Role : IOrmEntity
    { 
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
