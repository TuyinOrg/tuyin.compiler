using Tuyin.IR.Compiler.Parser;
using Tuyin.IR.Compiler.Parser.Expressions;

namespace Tuyin.IR.Compiler.Tests
{
    internal class Line : BaseTest
    {
        private Token START;
        private Token MIDDLE;
        private Token END;

        private Production LINE = new Production("循环");

        protected override ProductionBase OnDefineGrammar()
        {
            LINE.Rule = (START & MIDDLE) & START;

            return LINE;
        }

        protected override void OnDefineLexer(Lexicon lexicon, ICollection<Token> skippedTokens)
        {
            START = lexicon.DefineToken(RegularExpression.Literal("start"));
            MIDDLE = lexicon.DefineToken(RegularExpression.Literal("middle"));
            END = lexicon.DefineToken(RegularExpression.Literal("end"));
        }
    }
}