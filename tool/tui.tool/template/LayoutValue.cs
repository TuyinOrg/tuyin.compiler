using LLParserLexerLib;

namespace tui.tool
{
    internal class LayoutValue : IAST
    {
        private LayoutValueType rate;
        private float value;

        public LayoutValue(LayoutValueType rate, TokenAST nt1_s)
            : this(rate, float.Parse(nt1_s.tokenStr)) 
        {
        }

        public LayoutValue(LayoutValueType rate, float value)
        {
            this.rate = rate;
            this.value = value;
        }
    }
}