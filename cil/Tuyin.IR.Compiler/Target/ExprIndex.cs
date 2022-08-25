using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprIndex : ExprRoot
    {
        private ExprRoot tmp_15_i;
        private ExprList nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprIndex;

        internal ExprIndex(SourceSpan sourceSpan, ExprRoot tmp_15_i, ExprList nt2_s)
            : base(sourceSpan)
        {
            this.tmp_15_i = tmp_15_i;
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            if (nt2_s.list.Count == 1)
                return new Element(tmp_15_i.ToIR(cache), nt2_s.list[0].ToIR(cache));

            Dictionary<Parameter, Expression> exps = new Dictionary<Parameter, Expression>();
            for (int i = 0; i < nt2_s.list.Count; i++)
                exps.Add(new Parameter(new Identifier(i.ToString())), nt2_s.list[i].ToIR(cache));

            return new Literal(exps);
        }
    }
}