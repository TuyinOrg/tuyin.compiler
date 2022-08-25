using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    public class Float : Reference
    {
        public Float(BigFloat value)
        {
            Value = value;
        }

        public override AstNodeType NodeType => AstNodeType.Float;

        public BigFloat Value { get; }

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