using Tuyin.IR.Compiler.Parser;
using Tuyin.IR.Compiler.Parser.Expressions;

namespace Tuyin.IR.Compiler.Tests
{
    internal class Recursion : BaseTest
    {
        private Token START;
        private Token START2;
        private Token MIDDLE;
        private Token END;
        private Token END2;
        private Production RECURSION = new Production("递归");

        protected override ProductionBase OnDefineGrammar()
        {
            // RECURSION.Rule = (START | START2) & MIDDLE & RECURSION.Optional() & (END | END2);
            RECURSION.Rule = START & START2 & MIDDLE & RECURSION.Optional() & END;
            return RECURSION;
        }

        protected override void OnDefineLexer(Lexicon lexicon, ICollection<Token> skippedTokens)
        {
            START = lexicon.DefineToken(RegularExpression.Literal("start"));
            START2 = lexicon.DefineToken(RegularExpression.Literal("reset"));
            MIDDLE = lexicon.DefineToken(RegularExpression.Literal("middle"));
            END = lexicon.DefineToken(RegularExpression.Literal("end"));
            END2 = lexicon.DefineToken(RegularExpression.Any());
        }
    }
}
