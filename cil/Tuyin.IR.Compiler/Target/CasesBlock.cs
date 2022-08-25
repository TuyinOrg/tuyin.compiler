using LLParserLexerLib;

namespace Tuyin.IR.Compiler.Target
{
    class CasesBlock : List<StmtCase>, IAST
    {
        public CasesBlock(StmtCase nt1_s)
        {
            this.Add(nt1_s);
        }

        internal new IAST Add(StmtCase nt1_s)
        {
            base.Add(nt1_s);
            return this;
        }
    }
}