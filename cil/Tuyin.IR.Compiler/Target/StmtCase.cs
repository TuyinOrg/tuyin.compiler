namespace Tuyin.IR.Compiler.Target
{
    class StmtCase : ISourceSpan
    {
        private ExprRoot nt2_s;
        private StmtList nt4_s;

        public StmtCase(SourceSpan sourceSpan, ExprRoot nt2_s, StmtList nt4_s)
        {
            this.nt2_s = nt2_s;
            this.nt4_s = nt4_s;

            StartIndex = sourceSpan.StartIndex;
            EndIndex = sourceSpan.EndIndex;
        }

        public int StartIndex { get; }

        public int EndIndex { get; }
    }
}