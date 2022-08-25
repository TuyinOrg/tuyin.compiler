using LLParserLexerLib;

namespace tui.tool
{
    internal class LayoutIndex : LayoutToken
    {
        private TokenAST nt1_s;

        public LayoutIndex(TokenAST nt1_s)
        {
            this.nt1_s = nt1_s;
        }

        public override string ToString()
        {
            return "mElement" + nt1_s.strRead;
        }
    }
}