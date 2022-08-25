using LLParserLexerLib;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Compiler.Target
{
    internal class StmtList : List<StmtRoot>, IAST, ISourceSpan
    {
        public int StartIndex => base[0].StartIndex;

        public int EndIndex => base[^1].EndIndex;

        internal StmtList(StmtRoot nt1_s)
        {
            base.Add(nt1_s);
        }

        internal new IAST Add(StmtRoot nt1_s)
        {
            base.Add(nt1_s);
            return this;
        }

        internal void Write(StatmentBuilder stmts)
        {
            stmts.StartScope(StartIndex);

            for (var i = 0; i < base.Count; i++)
                base[i].Write(stmts);

            stmts.EndScope(EndIndex);
        }
    }
}