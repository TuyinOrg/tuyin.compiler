using LLParserLexerLib;

namespace tui.tool.template
{
    internal class Member : List<TokenAST>, IAST
    {
        public Member(TokenAST nt1_s)
        {
            base.Add(nt1_s);
        }

        internal new IAST Add(TokenAST nt2_s)
        {
            base.Add(nt2_s);
            return this;
        }

        public override string ToString()
        {
            return $"{string.Join(".", this.Select(x => x.strRead))}";
        }
    }
}