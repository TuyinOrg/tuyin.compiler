using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class StmtContinue : StmtRoot
    {
        public override AstNodeType AstType => AstNodeType.StmtContinue;

        internal StmtContinue(SourceSpan sourceSpan)
            : base(sourceSpan) 
        {
        }

        internal override void Write(StatmentBuilder stmts)
        {
            stmts.Add(new Goto(stmts.Scope.Enter) { SourceSpan = new SourceSpan(this) });
        }
    }
}