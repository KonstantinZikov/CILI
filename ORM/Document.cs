using System;
using System.ComponentModel.DataAnnotations;

namespace ORM
{
    public class Document : IOrmEntity
    {
        public int Id { get; set; }

        [Required]
        public DateTime LastChangeTime { get; set; }

        public string Code { get; set; }

        [Required]
        public User UserId { get; set; }
    }
}
