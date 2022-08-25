using Tuyin.IR.Compiler.Parser.Expressions;
using System.Collections.Generic;

namespace Tuyin.IR.Compiler.Parser
{
    class Lexicon
    {
        public Token Eos { get; private set; }

        public Token Missing { get; private set; }

        public Token Empty { get; private set; }

        public List<Token> Tokens { get; }

        internal Token Invaild { get; private set; }

        public Lexicon()
        {
            Tokens = new List<Token>();

            Invaild = DefineToken(new EmptyExpression(), "invaild");
            Invaild.IsSkip = true;

            Eos = DefineToken(RegularExpression.Symbol('\0'), "ε");
            Empty = DefineToken(new EmptyExpression(), "empty", null);
            Missing = DefineToken(new EmptyExpression(), "missing");
        }

        public Token DefineToken(RegularExpression regex)
        {
            return DefineToken(regex, null, null);
        }

        public Token DefineToken(RegularExpression regex, string description)
        {
            return DefineToken(regex, description, null);
        }

        public Token DefineToken(RegularExpression regex, TokenColor color) 
        {
            return DefineToken(regex, null, color);
        }

        public Token DefineToken(RegularExpression regex, string description, TokenColor? color) 
        {
            var indexInState = Tokens.Count;
            var token = new Token((ushort)indexInState, regex, description, color);
            Tokens.Add(token);
            return token;
        }
    }
}
