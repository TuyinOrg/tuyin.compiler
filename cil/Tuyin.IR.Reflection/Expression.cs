using E = System.Linq.Expressions.Expression;

namespace Tuyin.IR.Reflection
{
    public abstract class Expression : AstNode
    {
        public virtual E ConstantExpression { get; }
    }
}
