using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprList : IAST
    {
        internal List<ExprRoot> list;

        public ExprList()
        {
        }

        public ExprList(ExprRoot nt1_s)
        {
            list = new List<ExprRoot>();
            list.Add(nt1_s);
        }

        internal IAST Add(ExprRoot nt2_s)
        {
            list.Add(nt2_s);
            return this;
        }
    }
}