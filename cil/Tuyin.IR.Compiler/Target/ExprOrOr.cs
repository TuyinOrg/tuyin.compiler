using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprOrOr : ExprRoot
    {
        private ExprRoot tmp_5_i;
        private ExprRoot nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprOrOr;

        internal ExprOrOr(SourceSpan sourceSpan, ExprRoot tmp_5_i, ExprRoot nt2_s)
            : base(sourceSpan)
        {
            this.tmp_5_i = tmp_5_i;
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Or(tmp_5_i.ToIR(cache), nt2_s.ToIR(cache));
        }
    }
}