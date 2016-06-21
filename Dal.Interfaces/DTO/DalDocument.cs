using System;

namespace DAL.Interfaces.DTO
{
    class DalDocument
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime LastChangeTime { get; set; }
        public string Code { get; set; }
    }
}
