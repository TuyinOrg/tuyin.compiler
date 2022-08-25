using Tuyin.IR.Reflection;

namespace Tuyin.IR.Compiler.Target
{
    internal class StmtBlock : StmtScope
    {
        private StmtList nt2_s;

        public override AstNodeType AstType => AstNodeType.StmtBlock;

        public StmtBlock(SourceSpan sourceSpan, StmtList nt2_s)
            : base(sourceSpan)
        {
            this.nt2_s = nt2_s;
        }

        internal override void Write(StatmentBuilder stmts)
        {
            stmts.StartScope(StartIndex);
            nt2_s.Write(stmts);
            stmts.EndScope(EndIndex);
        }
    }
}