using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprNamed
    {
        internal TokenAST nt1_s;
        internal ExprRoot nt3_s;

        public ExprNamed(TokenAST nt1_s, ExprRoot nt3_s)
        {
            this.nt1_s = nt1_s;
            this.nt3_s = nt3_s;
        }
    }
}