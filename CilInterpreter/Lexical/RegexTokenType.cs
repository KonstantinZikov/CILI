using System.Text.RegularExpressions;

namespace CilInterpreter
{
    internal class RegexTokenType : ITokenType
    {
        public Regex Regex { get; private set; }
        public RegexTokenType(string regex)
        {
            Regex = new Regex(regex,RegexOptions.Singleline);
        }
        public bool Is(string word) =>
            Regex.IsMatch(word);           
    }
}
