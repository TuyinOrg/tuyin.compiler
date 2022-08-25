using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class StmtBreak : StmtRoot
    {
        public override AstNodeType AstType => AstNodeType.StmtBreak;

        internal StmtBreak(SourceSpan sourceSpan)
            : base(sourceSpan) 
        {
        }

        internal override void Write(StatmentBuilder stmts)
        {
            stmts.Add(new Goto(stmts.Scope.Exit) { SourceSpan = new SourceSpan(this) });
        }
    }
}