using System;

namespace DAL.Interfaces.DTO
{
    class DalDocument
    {
        public int UserId { get; set; }
        public DateTime LastChangeTime { get; set; }
        public string Code { get; set; }
    }
}
