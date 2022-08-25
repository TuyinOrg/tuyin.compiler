using LLParserLexerLib;

namespace tui.tool.template
{
    internal class LayoutLine : List<LayoutItem>, IAST
    {
        public LayoutLine(LayoutItem nt1_s)
        {
            base.Add(nt1_s);
        }

        internal new IAST Add(LayoutItem nt1_s)
        {
            base.Add(nt1_s);
            return this;
        }
    }
}