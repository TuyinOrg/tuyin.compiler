using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprSub : ExprRoot
    {
        private ExprRoot tmp_13_i;
        private ExprRoot nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprSub;

        public ExprSub(SourceSpan sourceSpan, ExprRoot tmp_13_i, ExprRoot nt2_s)
            : base(sourceSpan)
        {
            this.tmp_13_i = tmp_13_i;
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Sub(tmp_13_i.ToIR(cache), nt2_s.ToIR(cache));
        }
    }
}