using Tuyin.IR.Reflection;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprPlus : ExprRoot
    {
        private ExprRoot nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprPlus;

        internal ExprPlus(SourceSpan sourceSpan, ExprRoot nt2_s)
            : base(sourceSpan)
        {
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return nt2_s.ToIR(cache);
        }
    }
}