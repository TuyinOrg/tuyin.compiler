using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;
using String = Tuyin.IR.Reflection.Instructions.String;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprArray : ExprRoot
    {
        private ExprList nt2_s;

        public override AstNodeType AstType => AstNodeType.ExprArray;

        internal ExprArray(SourceSpan sourceSpan, ExprList nt2_s)
            : base(sourceSpan)
        {
            this.nt2_s = nt2_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            var alloc = new Identifier();
            for (var i = 0; i < nt2_s.list.Count; i++) 
            {
                var index = new String(i.ToString());
                var member = new Member(alloc, index);
                var exp = nt2_s.list[i].ToIR(cache);
                var set = new Store(member, exp);
                set.SourceSpan = new SourceSpan(nt2_s.list[i]);
                cache.Add(set);
            }

            return alloc;
        }
    }
}