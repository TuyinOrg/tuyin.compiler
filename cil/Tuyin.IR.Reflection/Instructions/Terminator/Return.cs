using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    public class Return : Statment
    {
        public Return(Expression expression)
        {
            Expression = expression;
        }

        public override AstNodeType NodeType => AstNodeType.Return;

        public Expression Expression { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            if (Expression != null)
                foreach (var node in Expression.GetNodes())
                    yield return node;

            yield return this;
        }

        public override string ToString()
        {
            return $"ret {Expression}";
        }
    }
}
