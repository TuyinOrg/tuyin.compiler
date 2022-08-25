using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    public class And : Expression
    {
        public Expression Left { get; }

        public Expression Right { get; }

        public And(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override AstNodeType NodeType => AstNodeType.And;

        public override IEnumerable<AstNode> GetNodes()
        {
            foreach (var item in Left.GetNodes())
                yield return item;

            foreach (var item in Right.GetNodes())
                yield return item;

            yield return this;
        }

        public override string ToString()
        {
            return $"and {Left},{Right}";
        }
    }
}