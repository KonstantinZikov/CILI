using System;

namespace BLL.Interface.Entities
{
    public class DocumentEntity : BllEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime LastChangeTime { get; set; }
        public string Code { get; set; }
        public bool IsExample { get; set; }
        public string Description { get; set; }
    }
}
