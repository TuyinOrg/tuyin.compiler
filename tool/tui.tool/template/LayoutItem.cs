using System.Text;

namespace tui.tool.template
{
    internal class LayoutItem : AstNode
    {
        private LayoutToken nt1_s;
        private LayoutValue nt3_s;

        public LayoutItem(LayoutToken nt1_s, LayoutValue nt3_s)
        {
            this.nt1_s = nt1_s;
            this.nt3_s = nt3_s;
        }

        public override void Create(TemplateControlBuilder sb)
        {
            
        }
    }
}