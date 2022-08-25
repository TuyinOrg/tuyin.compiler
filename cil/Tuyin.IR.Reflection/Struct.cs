using System.Collections.Generic;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Reflection
{
    internal class Struct : Statment, IExternal
    {
        public Visibility Visibility { get; }

        public Identifier Identifier { get; }

        public override AstNodeType NodeType => AstNodeType.Struct;

        public override IEnumerable<AstNode> GetNodes()
        {
            throw new System.NotImplementedException();
        }
    }
}
