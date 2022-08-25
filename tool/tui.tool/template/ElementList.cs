using LLParserLexerLib;

namespace tui.tool.template
{
    internal class ElementList : List<Element>, IAST
    {
        public ElementList() 
        {
        }

        internal new IAST Add(Element nt1_s)
        {
            base.Add(nt1_s);
            return this;
        }
    }
}