using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class StmtReturn : StmtRoot
    {
        private ExprRoot nt2_s;

        public override AstNodeType AstType => AstNodeType.StmtReturn;

        internal StmtReturn(SourceSpan sourceSpan, ExprRoot nt2_s)
            : base(sourceSpan)
        {
            this.nt2_s = nt2_s;
        }

        internal override void Write(StatmentBuilder stmts)
        {
            var ret = new Return(nt2_s.ToIR(stmts));
            ret.SourceSpan = new SourceSpan(this);
            stmts.Add(ret);
        }
    }
}