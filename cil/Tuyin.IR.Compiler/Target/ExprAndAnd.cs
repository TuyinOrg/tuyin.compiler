using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprAndAnd : ExprRoot
    {
        private ExprRoot tmp_6_i;
        private ExprRoot nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprAndAnd;

        public ExprAndAnd(SourceSpan sourceSpan, ExprRoot tmp_6_i, ExprRoot nt2_s)
            : base(sourceSpan)
        {
            this.tmp_6_i = tmp_6_i;
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new And(tmp_6_i.ToIR(cache), nt2_s.ToIR(cache));
        }
    }
}