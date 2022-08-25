using Tuyin.IR.Reflection;

namespace Tuyin.IR.Compiler.Target
{
    internal abstract class ExprNum : ExprRoot
    {
        protected ExprNum(SourceSpan sourceSpan)
            : base(sourceSpan) 
        {
        }
    }
}