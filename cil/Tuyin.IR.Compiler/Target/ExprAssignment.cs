using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprAssignment : ExprRoot
    {
        private ExprRoot tmp_12_i;
        private ExprRoot nt2_s;

        public ExprAssignment(SourceSpan sourceSpan, ExprRoot tmp_12_i, ExprRoot nt2_s) 
            : base(sourceSpan)
        {
            this.tmp_12_i = tmp_12_i;
            this.nt2_s = nt2_s;
        }

        public override AstNodeType AstType => AstNodeType.ExprAssignment;

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            var id = tmp_12_i.ToIR(cache);
            var set = new Store(id, nt2_s.ToIR(cache));
            set.SourceSpan = new SourceSpan(this);
            cache.Add(set);
            return id;
        }
    }
}