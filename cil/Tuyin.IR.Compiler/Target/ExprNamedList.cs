using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprNamedList : IAST
    {
        internal List<ExprNamed> list;

        public ExprNamedList(ExprNamed exprNamed)
        {
            list = new List<ExprNamed>();
            list.Add(exprNamed);
        }

        internal IAST Add(ExprNamed exprNamed)
        {
            list.Add(exprNamed);
            return this;
        }
    }
}