using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Compiler.Target
{
    internal class ExprTrue : ExprRoot
    {
        public override AstNodeType AstType => AstNodeType.ExprTrue;

        internal ExprTrue(SourceSpan sourceSpan)
            : base(sourceSpan) 
        {
        }

        protected override Expression CreateIR(StatmentBuilder cache)
        {
            return new Integer(1);
        }
    }
}