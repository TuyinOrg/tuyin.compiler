using Tuyin.IR.Compiler.Parser;
using Tuyin.IR.Compiler.Parser.Expressions;

namespace Tuyin.IR.Compiler.Tests
{
    internal class Optional : BaseTest
    {
        private Token START;
        private Token MIDDLE;
        private Token END;

        protected override ProductionBase OnDefineGrammar()
        {
            return START & (MIDDLE & END.Optional());
        }

        protected override void OnDefineLexer(Lexicon lexicon, ICollection<Token> skippedTokens)
        {
            START = lexicon.DefineToken(RegularExpression.Literal("start"));
            MIDDLE = lexicon.DefineToken(RegularExpression.Literal("middle"));
            END = lexicon.DefineToken(RegularExpression.Literal("end"));
        }
    }
}
