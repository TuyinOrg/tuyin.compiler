using Tuyin.IR.Compiler.Parser;
using Tuyin.IR.Compiler.Parser.Expressions;

namespace Tuyin.IR.Compiler.Tests
{
    internal class Loop : BaseTest
    {
        private Token START;
        private Token MIDDLE;
        private Token END;

        private Production LOOP = new Production("循环");

        protected override ProductionBase OnDefineGrammar()
        {
            LOOP.Rule = LOOP.Optional() & (START & MIDDLE & END) & LOOP.Optional();

            return LOOP;
        }

        protected override void OnDefineLexer(Lexicon lexicon, ICollection<Token> skippedTokens)
        {
            START = lexicon.DefineToken(RegularExpression.Literal("start"));
            MIDDLE = lexicon.DefineToken(RegularExpression.Literal("middle"));
            END = lexicon.DefineToken(RegularExpression.Literal("end"));
        }
    }
}