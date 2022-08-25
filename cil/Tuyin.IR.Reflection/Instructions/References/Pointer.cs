using System;
using System.Collections.Generic;
using System.Text;

namespace Tuyin.IR.Reflection.Instructions
{
    internal class Pointer : Reference
    {
        public Pointer(Expression source)
        {
            Source = source;
        }

        public override AstNodeType NodeType => AstNodeType.Pointer;

        public Expression Source { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            foreach (var item in Source.GetNodes())
                yield return item;

            yield return this;
        }
    }
}
