using CilInterpreter.Lexical;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CilInterpreter
{
    internal static class LexicalAnalizer
    {
        public static List<Token> Analize(string code)
        {         
            var tokens = code.CompressWhitespaces().DivideToTokens();
            for (int i = 0; i < tokens.Count; i++)
                if (tokens[i].Type == TokenTypePool.Identifier)
                    tokens[i] = ResolveIdentifier(tokens[i]);
            return tokens;
        }

        private static string CompressWhitespaces(this string code)
        {
            code = code.Replace("\r","");
            var result = new StringBuilder(code.Length);
            bool isWhitespace = false;
            bool isNextLine = false;
            for(int i = 0; i < code.Length; i++)
            {
                if (char.IsWhiteSpace(code[i]))
                    if (code[i] == '\n')
                        isNextLine = true;
                    else
                        isWhitespace = true;
                else
                {
                    if (isNextLine)
                        result.Append('\n');
                    else if (isWhitespace)
                        result.Append(' ');
                    result.Append(code[i]);
                    isWhitespace = isNextLine = false;
                }            
            }
            return result.ToString();
        }

        private static List<Token> DivideToTokens(this string compressedCode)
        {
            var result = new List<Token>();
            var word = new StringBuilder();
            int position = 0;
            var types = TokenTypePool.FastResolvingTypes;
            bool match = false;
            ITokenType lastType = null;
            int lineNumber = 1;
            while (position < compressedCode.Length)
            {
                word.Append(compressedCode[position]);
                match = false;
                foreach(var type in types)
                {
                    if (type.Is(word.ToString()))
                    {
                        lastType = type;
                        match = true;
                        break;
                    }
                }
                if (!match && lastType != null)
                {
                    if (lastType != TokenTypePool.Space && lastType!= TokenTypePool.NextLine)
                        result.Add(new Token(lastType, word.Remove(word.Length - 1, 1).ToString(), lineNumber));
                    if (lastType == TokenTypePool.NextLine)
                        lineNumber++;
                    lastType = null;
                    word.Clear();
                }
                else
                    position++;           
            }
            if (lastType == null)
            {
                string message = "";
                if (result.Count == 0)
                    message = "Unknown token in code start position.";
                else
                {
                    var token = result.Last();
                    message = $"Unknown token after element {token.Value} at line {token.Line}.";
                }
                throw new LexicalAnalizeException(message);
            }
            if (lastType != TokenTypePool.Space && lastType != TokenTypePool.NextLine)
                result.Add(new Token(lastType, word.ToString(), lineNumber));
            return result;
        }

        private static Token ResolveIdentifier(Token token)
        {
            foreach (var type in TokenTypePool.IdentifierResolvingTypes)
            {
                if (type.Is(token.Value))
                    return new Token(type, token.Value, token.Line);
            }
            return token;
        }
    }
}
