namespace BLL.Interface.Entities
{
    public class InstructionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSupported { get; set; }
    }
}
