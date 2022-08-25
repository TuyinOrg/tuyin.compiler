using Tuyin.IR.Compiler.Parser;
using Tuyin.IR.Compiler.Parser.Expressions;

namespace Tuyin.IR.Compiler.Tests
{
    internal class Conflict : BaseTest
    {
        private Token START;
        private Token MIDDLE;
        private Token END;
        private Token OTHER;

        private Production P = new Production("循环");

        protected override ProductionBase OnDefineGrammar()
        {
            P.Rule = //((START & MIDDLE) | (START & MIDDLE & START)) & START;
                     //(START & START) | START;
                    ((START & MIDDLE & END) | (START) | (START & END)) & OTHER;
             
            return P;
        }

        protected override void OnDefineLexer(Lexicon lexicon, ICollection<Token> skippedTokens)
        {
            START = lexicon.DefineToken(RegularExpression.Literal("start"));
            MIDDLE = lexicon.DefineToken(RegularExpression.Literal("middle"));
            END = lexicon.DefineToken(RegularExpression.Literal("end"));
            OTHER = lexicon.DefineToken(RegularExpression.Literal("other"));
        }
    }
}