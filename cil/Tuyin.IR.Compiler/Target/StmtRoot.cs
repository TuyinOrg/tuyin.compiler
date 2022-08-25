using Tuyin.IR.Reflection;

namespace Tuyin.IR.Compiler.Target
{
    internal abstract class StmtRoot : AstNode
    {
        protected StmtRoot(SourceSpan sourceSpan)
            : base(sourceSpan) 
        {
        }

        internal virtual void Write(StatmentBuilder stmts)
        {
        }
    }
}