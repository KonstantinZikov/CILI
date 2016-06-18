namespace DAL.Interfaces.DTO
{
    public class DalInstruction : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsSupported { get; set; }

        public string Description { get; set; }
    }
}
