using Tuyin.IR.Compiler.Parser;
using Tuyin.IR.Compiler.Parser.Expressions;

namespace Tuyin.IR.Compiler.Tests
{
    internal class Priority : BaseTest
    {
        private Token PLUS;
        private Token MUL;
        private Token ONE;

        protected override ProductionBase OnDefineGrammar()
        {
            var exp = new Production("exp");

            var one = new Production("one");
            one.Rule = ONE.AsTerminal();

            var mul = new Production("mul");
            mul.Rule = exp & MUL & exp;

            var plus = new Production("plus");
            plus.Rule = exp & PLUS & exp;

            exp.Rule = one | mul | plus;

            //exp.Rule =  (exp & MUL & exp) | (exp & PLUS & exp) | ONE.AsTerminal();


            return exp;
        }

        protected override void OnDefineLexer(Lexicon lexicon, ICollection<Token> skippedTokens)
        {
            PLUS = lexicon.DefineToken(RegularExpression.Literal("+"));
            MUL = lexicon.DefineToken(RegularExpression.Literal("*"));
            ONE = lexicon.DefineToken(RegularExpression.Literal("1"));
        }
    }
}
