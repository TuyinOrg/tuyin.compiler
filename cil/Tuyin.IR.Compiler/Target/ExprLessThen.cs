using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprLessThen : ExprRoot
    {
        private ExprRoot tmp_11_i;
        private ExprRoot nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprLessThen;

        internal ExprLessThen(SourceSpan sourceSpan, ExprRoot tmp_11_i, ExprRoot nt2_s)
            : base(sourceSpan)
        {
            this.tmp_11_i = tmp_11_i;
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new LessThen(tmp_11_i.ToIR(cache), nt2_s.ToIR(cache));
        }
    }
}