using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprPostInc : ExprRoot
    {
        private ExprRoot tmp_15_i;

        public override AstNodeType AstType => AstNodeType.ExprPostInc;

        internal ExprPostInc(SourceSpan sourceSpan, ExprRoot tmp_15_i)
            : base(sourceSpan)
        {
            this.tmp_15_i = tmp_15_i;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            var ir = tmp_15_i.ToIR(cache);
            var set = new Store(ir, new Add(ir, new Integer(1)));
            set.SourceSpan = new SourceSpan(tmp_15_i);
            cache.Add(set);
            return ir;
        }
    }
}