using LLParserLexerLib;

namespace tui.tool.template
{
    internal class ElementControl : Element
    {
        private TokenAST nt2_s;
        private TokenAST nt4_s;
        private PropertyList nt5_s;
        private AnimateList nt6_s;

        public ElementControl(TokenAST nt2_s, TokenAST nt4_s, PropertyList nt5_s, AnimateList nt6_s)
        {
            this.nt2_s = nt2_s;
            this.nt4_s = nt4_s;
            this.nt5_s = nt5_s;
            this.nt6_s = nt6_s;
        }

        public override void Create(TemplateControlBuilder sb)
        {
            var index = int.Parse(nt2_s.strRead);
            var control = nt4_s.strRead;
            var name = $"c{index}";

            sb.Define(name, control, $"{name} = new {control}() {{ {string.Join(",", nt5_s.Select(x => $"{x.PropertyName}={x.Value}"))} }}");
            for (var i = 0; i < nt6_s.Count; i++)
                nt6_s[i].Create(sb);
        }
    }
}