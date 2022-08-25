using Tuyin.IR.Reflection;

namespace Tuyin.IR.Compiler.Target
{
    internal abstract class ExprRoot : AstNode
    {
        private Expression ir;

        protected ExprRoot(SourceSpan sourceSpan)
            : base(sourceSpan) 
        {
        }

        internal Expression ToIR(StatmentBuilder cache) 
        {
            if (ir == null)
            {
                ir = CreateIR(cache);
                ir.SourceSpan = new SourceSpan(this);
            }

            return ir;
        }

        protected abstract Expression CreateIR(StatmentBuilder cache);
    }
}