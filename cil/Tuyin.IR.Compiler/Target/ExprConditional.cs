using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprConditional : ExprRoot
    {
        private ExprRoot nt1_s;
        private ExprRoot nt3_s;
        private ExprRoot nt5_s;

        public override AstNodeType AstType => AstNodeType.ExprConditional;

        internal ExprConditional(SourceSpan sourceSpan, ExprRoot nt1_s, ExprRoot nt3_s, ExprRoot nt5_s)
            : base(sourceSpan)
        {
            this.nt1_s = nt1_s;
            this.nt3_s = nt3_s;
            this.nt5_s = nt5_s;
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            var id = new Identifier();
            var label1 = new Label();
            var label2 = new Label();

            var first = new Store(id, nt3_s.ToIR(cache));
            var follow = new Store(id, nt5_s.ToIR(cache));
            first.SourceSpan = new SourceSpan(nt3_s);
            follow.SourceSpan = new SourceSpan(nt5_s);

            var brf = new Test(label1, nt1_s.ToIR(cache));
            brf.SourceSpan = new SourceSpan(nt1_s);
            cache.Add(brf);
            cache.Add(first);
            cache.Add(new Goto(label2));
            cache.DefineLabel(label1);
            cache.Add(follow);
            cache.DefineLabel(label2);

            return id;
        }
    }
}