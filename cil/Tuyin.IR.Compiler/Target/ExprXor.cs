using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprXor : ExprRoot
    {
        private ExprRoot tmp_8_i;
        private ExprRoot nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprXor;

        internal ExprXor(SourceSpan sourceSpan, ExprRoot tmp_8_i, ExprRoot nt2_s)
            : base(sourceSpan)
        {
            this.tmp_8_i = tmp_8_i;
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Xor(tmp_8_i.ToIR(cache), nt2_s.ToIR(cache));
        }
    }
}