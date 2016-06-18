using System.ComponentModel.DataAnnotations;

namespace ORM
{
    public class Instruction : IOrmEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsSupported { get; set; }

        public string Description { get; set; }
    }
}
