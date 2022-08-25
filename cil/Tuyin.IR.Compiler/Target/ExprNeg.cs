using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprNeg : ExprRoot
    {
        private ExprRoot nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprNeg;

        internal ExprNeg(SourceSpan sourceSpan, ExprRoot nt2_s)
            : base(sourceSpan)
        {
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Neg(nt2_s.ToIR(cache));
        }
    }
}