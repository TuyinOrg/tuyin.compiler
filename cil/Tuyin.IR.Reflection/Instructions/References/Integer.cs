using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    public class Integer : Reference
    {
        public Integer(BigInteger value)
        {
            Value = value;
        }

        public override AstNodeType NodeType => AstNodeType.Integer;

        public BigInteger Value { get; }

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