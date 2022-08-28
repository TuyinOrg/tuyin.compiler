using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    public class Integer : Reference
    {
        public Integer(int value)
        {
            Value = value;
        }

        public override AstNodeType NodeType => AstNodeType.Integer;

        public int Value { get; }

        public override System.Linq.Expressions.Expression ConstantExpression => System.Linq.Expressions.Expression.Constant(Value);

        public override IEnumerable<AstNode> GetNodes()
        {
            yield return this;
        }

        public override string ToString()
        {
            return $"ldc {Value}";
        }
    }
}