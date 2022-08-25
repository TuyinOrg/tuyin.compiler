using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprNotEqual : ExprRoot
    {
        private ExprRoot tmp_10_i;
        private ExprRoot nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprNotEqual;

        internal ExprNotEqual(SourceSpan sourceSpan, ExprRoot tmp_10_i, ExprRoot nt2_s)
            : base(sourceSpan)
        {
            this.tmp_10_i = tmp_10_i;
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Neg(new Equal(tmp_10_i.ToIR(cache), nt2_s.ToIR(cache)));
        }
    }
}