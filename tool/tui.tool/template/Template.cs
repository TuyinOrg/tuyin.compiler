using tui.tool.template;

namespace tui.tool
{
    internal class Template : AstNode
    {
        private ElementList nt1_s;
        private TemplateLayout nt2_s;

        public Template(ElementList nt1_s, TemplateLayout nt2_s)
        {
            this.nt1_s = nt1_s;
            this.nt2_s = nt2_s;
        }

        public override void Create(TemplateControlBuilder sb)
        {
            for (var i = 0; i < nt1_s.Count; i++)
                nt1_s[i].Create(sb);

            if (nt2_s == null)
            {
                sb.Layout(LayoutType.Canvas);
            }
            else
            {
                for (var i = 0; i < nt2_s.Count; i++)
                {
                    var line = nt2_s[i];
                    for (var x = 0; x < line.Count; x++)
                    {
                        line[x].Create(sb);
                    }
                }
            }
        }
    }
}