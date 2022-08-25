using LLParserLexerLib;

namespace tui.tool.template
{
    internal class InputEvent : IAST
    {
        private EventType nt1_s;
        private int nt2_s;

        public EventType EventType => nt1_s;

        public int Value => nt2_s;

        public InputEvent(EventType nt1_s, int nt2_s)
        {
            this.nt1_s = nt1_s;
            this.nt2_s = nt2_s;
        }
    }
}