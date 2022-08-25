using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    public class Store : Statment
    {
        public Store(Expression source, Expression value)
        {
            Source = source;
            Value = value;
        }

        public override AstNodeType NodeType => AstNodeType.Store;

        public Expression Source { get; }

        public Expression Value { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            foreach (var item in Value.GetNodes())
                yield return item;

            foreach (var item in Source.GetNodes())
                yield return item;

            yield return this;
        }

        public override string ToString()
        {
            return $"store {Source},{Value}";
        }
    }
}
