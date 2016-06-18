namespace CilPlayground.Models
{
    public enum Type
    {
        Instruction = 1,
        Directive,
        KeyWord
    }

    public class InstructionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsSupported { get; set; }
        
    }
}