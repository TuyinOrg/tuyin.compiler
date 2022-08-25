using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprOr : ExprRoot
    {
        private ExprRoot tmp_7_i;
        private ExprRoot nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprOr;

        internal ExprOr(SourceSpan sourceSpan, ExprRoot tmp_7_i, ExprRoot nt2_s)
            : base(sourceSpan)
        {
            this.tmp_7_i = tmp_7_i;
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Or(tmp_7_i.ToIR(cache), nt2_s.ToIR(cache));
        }
    }
}