using Tuyin.IR.Reflection;

namespace Tuyin.IR.Compiler.Target
{
    internal class StmtExpression : StmtRoot
    {
        private ExprRoot nt1_s;

        public override AstNodeType AstType => AstNodeType.StmtExpression;

        internal StmtExpression(SourceSpan sourceSpan, ExprRoot nt1_s)
            : base(sourceSpan)
        {
            this.nt1_s = nt1_s;
        }

        internal override void Write(StatmentBuilder stmts)
        {
            nt1_s.ToIR(stmts);
        }
    }
}