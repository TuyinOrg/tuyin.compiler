namespace Tuyin.IR.Compiler.Target
{
    internal abstract class StmtScope : StmtRoot
    {
        protected StmtScope(SourceSpan sourceSpan) 
            : base(sourceSpan)
        {
        }
    }
}
