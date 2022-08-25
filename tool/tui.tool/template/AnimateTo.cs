using LLParserLexerLib;

namespace tui.tool.template
{
    internal class AnimateTo : Animate
    {
        private Member nt1_s;
        private TokenAST nt3_s;
        private TokenAST nt5_s;
        private TokenAST nt7_s;
        private InputEvent nt9_s;

        public AnimateTo(Member nt1_s, TokenAST nt3_s, TokenAST nt5_s, TokenAST nt7_s, InputEvent nt9_s)
        {
            this.nt1_s = nt1_s;
            this.nt3_s = nt3_s;
            this.nt5_s = nt5_s;
            this.nt7_s = nt7_s;
            this.nt9_s = nt9_s;
        }

        public override void Create(TemplateControlBuilder sb)
        {  
            var from = float.Parse(nt3_s.strRead);
            var to = float.Parse(nt5_s.strRead);
            var time = int.Parse(nt7_s.strRead);
            var diff = from - to;

            if (time == 0)
                sb.Event(nt9_s.EventType, nt9_s.Value, $"{nt1_s}={to}");
            else
                sb.Event(nt9_s.EventType, nt9_s.Value, $"{nt1_s}=timer.Elapsed < {time} ? {from}+{diff}*(timer.Elapsed-{time})");
        }
    }
}