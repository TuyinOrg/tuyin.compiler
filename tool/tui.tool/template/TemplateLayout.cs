using LLParserLexerLib;

namespace tui.tool.template
{
    internal class TemplateLayout : List<LayoutLine>, IAST
    {
        public TemplateLayout(LayoutLine nt1_s)
        {
            base.Add(nt1_s);
        }

        internal new IAST Add(LayoutLine nt2_s)
        {
            base.Add(nt2_s);
            return this;
        }
    }
}