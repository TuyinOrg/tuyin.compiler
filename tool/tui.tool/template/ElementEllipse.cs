using LLParserLexerLib;
using System.Text;

namespace tui.tool.template
{
    internal class ElementEllipse : Element
    {
        private TokenAST nt2_s;
        private TokenAST nt3_s;
        private TokenAST nt5_s;
        private TokenAST nt7_s;
        private TokenAST nt9_s;

        public ElementEllipse(TokenAST nt2_s, TokenAST nt3_s, TokenAST nt5_s, TokenAST nt7_s, TokenAST nt9_s)
        {
            this.nt2_s = nt2_s;
            this.nt3_s = nt3_s;
            this.nt5_s = nt5_s;
            this.nt7_s = nt7_s;
            this.nt9_s = nt9_s;
        }

        public override void Create(TemplateControlBuilder sb)
        {
            sb.Paint($"g.FillGeometry(new SolidBrush({Helper.CodeHexColor(nt2_s.strRead)}), new EllipseF({float.Parse(nt3_s.strRead)},{float.Parse(nt5_s.strRead)},{float.Parse(nt7_s.strRead)},{float.Parse(nt9_s.strRead)}));");
        }
    }
}