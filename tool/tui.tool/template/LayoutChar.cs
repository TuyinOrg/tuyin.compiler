using LLParserLexerLib;

namespace tui.tool
{
    internal class LayoutChar : LayoutToken
    {
        private TokenAST nt1_s;

        public LayoutChar(TokenAST nt1_s)
        {
            this.nt1_s = nt1_s;
        }

        public override string ToString()
        {
            switch (nt1_s.strRead) 
            {
                case "-": return "";
                case "|": return "";
            }

            throw new NotSupportedException(nt1_s.strRead);
        }
    }
}