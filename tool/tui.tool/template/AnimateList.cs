using LLParserLexerLib;
using tui.tool.template;

namespace tui.tool
{
    internal class AnimateList : List<Animate>, IAST
    {
        internal new IAST Add(Animate nt2_s)
        {
            base.Add(nt2_s);
            return this;
        }
    }
}