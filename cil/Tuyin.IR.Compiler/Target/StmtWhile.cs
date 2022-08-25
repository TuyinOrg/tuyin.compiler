using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class StmtWhile : StmtScope
    {
        private ExprRoot nt3_s;
        private StmtRoot nt5_s;

        public override AstNodeType AstType => AstNodeType.StmtWhile;

        internal StmtWhile(SourceSpan sourceSpan, ExprRoot nt3_s, StmtRoot nt5_s)
            : base(sourceSpan)
        {
            this.nt3_s = nt3_s;
            this.nt5_s = nt5_s;
        }

        internal override void Write(StatmentBuilder stmts)
        {
            stmts.StartScope(StartIndex);

            stmts.DefineLabel(stmts.Scope.Enter);
            var brf = new Test(stmts.Scope.Exit, nt3_s.ToIR(stmts));
            brf.SourceSpan = new SourceSpan(nt3_s);
            stmts.Add(brf);
            nt5_s.Write(stmts);
            stmts.Add(new Goto(stmts.Scope.Enter));
            stmts.DefineLabel(stmts.Scope.Exit);

            stmts.EndScope(EndIndex);
        }
    }
}