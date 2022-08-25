using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprLiteral : ExprRoot
    {
        private ExprNamedList nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprLiteral;

        internal ExprLiteral(SourceSpan sourceSpan, ExprNamedList nt2_s)
            : base(sourceSpan)
        {
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Literal(nt2_s.list.ToDictionary(x => new Parameter(new Identifier(x.nt1_s.strRead) { SourceSpan = new SourceSpan(x.nt1_s) }), x => x.nt3_s.ToIR(cache)));
        }
    }
}