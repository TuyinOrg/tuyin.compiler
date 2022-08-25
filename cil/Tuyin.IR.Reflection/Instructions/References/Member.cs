using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    public class Member : Reference
    {
        public Member(Expression src, String field)
        {
            Source = src;
            Field = field;
        }

        public override AstNodeType NodeType => AstNodeType.Member;

        public Expression Source { get; }

        public String Field { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            foreach(var node in Source.GetNodes())
                yield return node;

            yield return this;
        }

        public override string ToString()
        {
            return $"ldfld {Field.Value}";
        }
    }
}
