using LLParserLexerLib;

namespace tui.tool.template
{
    internal class PropertyList : List<Property>, IAST
    {
        internal new IAST Add(Property nt2_s)
        {
            base.Add(nt2_s);
            return this;
        }
    }
}