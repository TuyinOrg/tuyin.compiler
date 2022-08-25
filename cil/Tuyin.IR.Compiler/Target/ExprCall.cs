using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprCall : ExprRoot
    {
        private ExprRoot tmp_15_i;
        private ExprList nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprCall;

        internal ExprCall(SourceSpan sourceSpan, ExprRoot tmp_15_i, ExprList nt2_s)
            : base(sourceSpan)
        {
            this.tmp_15_i = tmp_15_i;
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Call(tmp_15_i.ToIR(cache), nt2_s.list.Select(x => x.ToIR(cache)));
        }
    }
}