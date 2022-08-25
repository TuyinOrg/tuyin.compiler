using Tuyin.IR.Compiler.Parser;
using Tuyin.IR.Compiler.Parser.Expressions;

namespace Tuyin.IR.Compiler.Tests
{
    internal class OrAnd : BaseTest
    {
        private Token START;
        private Token START2;
        private Token MIDDLE;
        private Token END;
        private Token END2;

        protected override ProductionBase OnDefineGrammar()
        {
            return (START | (START2 & MIDDLE)) & MIDDLE & (END | (END2 & MIDDLE));
        }

        protected override void OnDefineLexer(Lexicon lexicon, ICollection<Token> skippedTokens)
        {
            START = lexicon.DefineToken(RegularExpression.Literal("start"));
            START2 = lexicon.DefineToken(RegularExpression.Literal("reset"));
            MIDDLE = lexicon.DefineToken(RegularExpression.Literal("middle"));
            END = lexicon.DefineToken(RegularExpression.Literal("end"));
            END2 = lexicon.DefineToken(RegularExpression.Literal("ddd"));
        }
    }
}
