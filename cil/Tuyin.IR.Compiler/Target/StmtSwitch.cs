namespace Tuyin.IR.Compiler.Target
{
    internal class StmtSwitch : StmtRoot
    {
        private ExprRoot nt3_s;
        private CasesBlock nt6_s;

        public override AstNodeType AstType => AstNodeType.StmtSwitch;

        public StmtSwitch(SourceSpan sourceSpan, ExprRoot nt3_s, CasesBlock nt6_s)
            : base(sourceSpan)
        {
            this.nt3_s = nt3_s;
            this.nt6_s = nt6_s;
        }
    }
}