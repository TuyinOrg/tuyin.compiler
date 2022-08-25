using LLParserLexerLib;

namespace tui.tool.template
{
    abstract class AstNode : IAST
    {
        public abstract void Create(TemplateControlBuilder sb);
    }
}
