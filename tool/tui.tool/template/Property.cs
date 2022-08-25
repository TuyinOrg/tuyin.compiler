using LLParserLexerLib;

namespace tui.tool.template
{
    internal class Property : Element
    {
        private TokenAST nt1_s;
        private TokenAST nt3_s;

        public string PropertyName => nt1_s.strRead;

        public float Value => float.Parse(nt3_s.strRead);

        public Property(TokenAST nt1_s, TokenAST nt3_s)
        {
            this.nt1_s = nt1_s;
            this.nt3_s = nt3_s;
        }

        public override void Create(TemplateControlBuilder sb)
        {
            sb.Property(nt1_s.strRead, float.Parse(nt3_s.strRead));
        }
    }
}