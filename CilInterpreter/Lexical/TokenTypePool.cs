using CilInterpreter.Lexical;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CilInterpreter
{
    internal static class TokenTypePool
    {
        static TokenTypePool()
        {           
            Number = new RegexTokenType(@"^\d+\z");
            Space = new RegexTokenType(@"^ \z");
            NextLine = new RegexTokenType(@"^\n\z");
            Identifier = new RegexTokenType(@"^[a-zA-Z_]([a-zA-Z_]|\d)*(\.([a-zA-Z_]|\d)*)*\z");
            Directive = new RegexTokenType(@"^\.[a-zA-Z_]([a-zA-Z_]|\d)*\z");
            AssemblyRef = new RegexTokenType(@"\[[a-zA-Z_]([a-zA-Z_]|\d)*\]");
            String = new RegexTokenType(@"^"".*""\z");
            PartOfString = new RegexTokenType(@"^""[^""]*\z");
            Delimeter = new PredefinedTokenType(SymbolPool.SymbolStrings);

            Command = new PredefinedTokenType(CommandPool.CommandStrings);
            Keyword = new PredefinedTokenType(KeywordPool.KeywordStrings);
            Type = new PredefinedTokenType(TypePool.TypeStrings);

            FastResolvingTypes = new ReadOnlyCollection<ITokenType>(
                new List<ITokenType>
                {
                    Number,
                    Identifier,
                    Directive,
                    Space,
                    NextLine,
                    Delimeter,
                    String,
                    PartOfString
                });

            IdentifierResolvingTypes = new ReadOnlyCollection<ITokenType>(
                new List<ITokenType>
                {
                    Command,
                    Keyword,
                    Type,
                });
        }

        public static ReadOnlyCollection<ITokenType> FastResolvingTypes { get; private set; }
        public static ReadOnlyCollection<ITokenType> IdentifierResolvingTypes { get; private set; }
        //TokenTypes
        public static ITokenType Number { get; private set; }
        public static ITokenType Identifier { get; private set; }      
        public static ITokenType Directive { get; private set; }
        public static ITokenType Space { get; private set; }
        public static ITokenType NextLine { get; private set; }
        public static ITokenType AssemblyRef { get; private set; }
        public static ITokenType Delimeter { get; private set; }
        public static ITokenType String { get; private set; }
        public static ITokenType PartOfString { get; private set; }

        public static ITokenType Command { get; private set; }
        public static ITokenType Keyword { get; private set; }
        public static ITokenType Type { get; private set; }
    }
}
