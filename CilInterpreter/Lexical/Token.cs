namespace CilInterpreter
{
    internal class Token
    {
        public ITokenType Type { get; private set; }
        public string Value { get; private set; }
        public int Line { get; private set; }
        public Token(ITokenType type, string value, int line)
        {
            Type = type;
            Value = value;
            Line = line;
        }
    }
}
