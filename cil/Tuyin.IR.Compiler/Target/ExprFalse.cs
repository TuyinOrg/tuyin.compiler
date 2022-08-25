using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprFalse : ExprRoot
    {
        public override AstNodeType AstType => AstNodeType.ExprFalse;

        internal ExprFalse(SourceSpan sourceSpan)
            : base(sourceSpan) 
        {
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Integer(0);
        }
    }
}