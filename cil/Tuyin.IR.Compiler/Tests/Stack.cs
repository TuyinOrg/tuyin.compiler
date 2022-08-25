using Tuyin.IR.Compiler.Parser;
using RE = Tuyin.IR.Compiler.Parser.Expressions.RegularExpression;

namespace Tuyin.IR.Compiler.Tests
{
    internal class Stack : BaseTest
    {
        private Token TOKEN1;
        private Token TOKEN2;
        private Token TOKEN3;

        protected override ProductionBase OnDefineGrammar()
        {
            return TOKEN1 & TOKEN2 | TOKEN3;
        }

        protected override void OnDefineLexer(Lexicon lex, ICollection<Token> skippedTokens)
        {
            TOKEN1 = lex.DefineToken(RE.Symbol('c'));
            TOKEN2 = lex.DefineToken(RE.Symbol('b'));
            TOKEN3 = lex.DefineToken(RE.Symbol('a'));
        }
    }
}
