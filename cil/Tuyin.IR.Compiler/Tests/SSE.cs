using Tuyin.IR.Compiler.Parser;
using RE = Tuyin.IR.Compiler.Parser.Expressions.RegularExpression;

namespace Tuyin.IR.Compiler.Tests
{
    internal class SSE : BaseTest
    {
        private Token TOKEN1;
        private Token TOKEN2;

        protected override ProductionBase OnDefineGrammar()
        {
            return TOKEN1 | TOKEN2;
        }

        protected override void OnDefineLexer(Lexicon lex, ICollection<Token> skippedTokens)
        {
            TOKEN1 = lex.DefineToken(RE.Symbol('c'));
            TOKEN2 = lex.DefineToken(RE.Any());
        }
    }
}
