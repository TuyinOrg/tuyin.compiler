using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    public class Neg : Expression
    {
        public Neg(Expression src)
        {
            Source = src;
        }

        public override AstNodeType NodeType => AstNodeType.Neg;

        public Expression Source { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            foreach (var node in Source.GetNodes())
                yield return node;

            yield return this;
        }

        public override string ToString()
        {
            return $"neg {Source}";
        }
    }
}