using libtui.controls;
using LLParserLexerLib;
using System.Text;

namespace tui.tool.template
{
    partial class TemplateControlParser
    {
        public string TemplateName { get; private set; }

        public TemplateControlParser(string templateName)
            : base(0)
        {
            TemplateName = templateName;
        }

        public static string Parse(string name, string source)
        {
            using (var lexer = new LexReader(new StringReader(source), String.Empty))
            {
                return new TemplateControlParser(name).Parse(lexer);
            }
        }

        internal string Parse(LexReader rd)
        {
            this.init(rd);
            var v = this.template(null);
            var tb = new TemplateControlBuilder(TemplateName);
            v.Create(tb);
            return tb.ToString();
        }


        public int CharToKeys(char c)
        {
            var key = (Keys)Enum.Parse(typeof(Keys), ((int)c).ToString());
            return (int)key;
        }

        private void ReadRegex(LexReader rd, StringBuilder sb)
        {
            const string flags = "|?*+~-.()[]{}\r\n\t\b";

            // 遇到标记符停止
            int last = 0;
            for (;;)
            {
                char ch = (char)rd.Read().ch;
                if (ch == -1) throw new Exception("EOF in string");

                if (last != '\\' && flags.Contains(ch))
                    break;

                if (ch != '\\')
                    sb.Append(ch);

                last = ch;
            }
        }


        private void ReadString(LexReader rd, StringBuilder sb)
        {
            int last = 0;
            for (;;)
            {
                int ch = rd.Read().ch;
                if (ch == -1) throw new Exception("EOF in string");

                if (last != '\\' && ch == '\'')
                    break;

                if (ch != '\\')
                    sb.Append((char)ch);

                last = ch;
            }
        }

        private void ReadComment(LexReader rd)
        {
            for (;;)
            {
                if (rd.Peek().ch == -1) throw new Exception("EOF in comment");
                if (rd.Read().ch == '*' && rd.Peek().ch == '/')
                {
                    rd.Read();
                    break;
                }
            }
        }
    }
}
