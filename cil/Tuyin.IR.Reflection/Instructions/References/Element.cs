using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    public class Element : Reference
    {
        public Element(Expression src, Expression index)
        {
            Source = src;
            Index = index;
        }

        public override AstNodeType NodeType => AstNodeType.Element;

        public Expression Source { get; }

        public Expression Index { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            foreach (var item in Source.GetNodes())
                yield return item;

            foreach (var item in Index.GetNodes())
                yield return item;

            yield return this;
        }

        public override string ToString()
        {
            return $"ldelem {Source},{Index}";
        }
    }
}
