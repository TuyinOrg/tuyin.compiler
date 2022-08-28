using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    public class Float : Reference
    {
        public Float(float value)
        {
            Value = value;
        }

        public override AstNodeType NodeType => AstNodeType.Float;

        public float Value { get; }

        public override System.Linq.Expressions.Expression ConstantExpression => System.Linq.Expressions.Expression.Constant(Value);

        public override IEnumerable<AstNode> GetNodes()
        {
            yield return this;
        }

        public override string ToString()
        {
            return $"ldr {Value}";
        }
    }
}