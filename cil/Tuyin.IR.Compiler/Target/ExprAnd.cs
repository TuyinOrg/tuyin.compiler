using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprAnd : ExprRoot
    {
        private ExprRoot tmp_9_i;
        private ExprRoot nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprAnd;

        internal ExprAnd(SourceSpan sourceSpan, ExprRoot tmp_9_i, ExprRoot nt2_s)
            : base(sourceSpan)
        {
            this.tmp_9_i = tmp_9_i;
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new And(tmp_9_i.ToIR(cache), nt2_s.ToIR(cache));
        }
    }
}