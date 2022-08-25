using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprPreDec : ExprRoot
    {
        private ExprRoot nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprPreDec;

        internal ExprPreDec(SourceSpan sourceSpan, ExprRoot nt2_s)
            : base(sourceSpan)
        {
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            if (!(nt2_s is ExprIdentifier))
                throw new NotImplementedException();

            var tmp = new Identifier();
            var ir = nt2_s.ToIR(cache);
            var set = new Store(tmp, new Add(ir, new Integer(-1)));
            set.SourceSpan = new SourceSpan(nt2_s);
            cache.Add(set);
            return tmp;
        }
    }
}