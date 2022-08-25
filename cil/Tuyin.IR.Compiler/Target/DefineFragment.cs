using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    internal class DefineFragment : SourceProduction
    {
        private TokenAST nt1_s;

        public DefineFragment(SourceSpan span, SourceProduction nt3_s, TokenAST nt1_s)
            : base(span, nt3_s)
        {
            this.nt1_s = nt1_s;
        }
    }
}