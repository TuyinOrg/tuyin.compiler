using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprRem : ExprRoot
    {
        private ExprRoot tmp_14_i;
        private ExprRoot nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprRem;

        internal ExprRem(SourceSpan sourceSpan, ExprRoot tmp_14_i, ExprRoot nt2_s)
            : base(sourceSpan)
        {
            this.tmp_14_i = tmp_14_i;
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Rem(tmp_14_i.ToIR(cache), nt2_s.ToIR(cache));
        }
    }
}