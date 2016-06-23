using System;
using System.ComponentModel.DataAnnotations;

namespace ORM
{
    public class Document : IOrmEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime LastChangeTime { get; set; }

        public bool IsExample { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
