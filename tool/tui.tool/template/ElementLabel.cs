using LLParserLexerLib;
using System.Text;

namespace tui.tool.template
{
    internal class ElementLabel : Element
    {
        private TokenAST nt1_s;

        public ElementLabel(TokenAST nt1_s)
        {
            this.nt1_s = nt1_s;
        }

        public override void Create(TemplateControlBuilder sb)
        {

        }
    }
}