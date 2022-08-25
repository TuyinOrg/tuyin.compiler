using System.Collections.Generic;
using System.Linq;

namespace Tuyin.IR.Reflection.Instructions
{
    public class Literal : Reference
    {
        public Literal(IReadOnlyDictionary<Parameter, Expression> members)
        {
            Members = new Dictionary<Parameter, Expression>(members);
        }

        public override AstNodeType NodeType => AstNodeType.Literal;

        public IReadOnlyDictionary<Parameter, Expression> Members { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            foreach (var member in Members)
                foreach (var node in member.Value.GetNodes())
                    yield return node;

            yield return this;
        }

        public override string ToString()
        {
            return $"struct\n" +
                string.Join("   \n", Members.Select(x => $"{x.Key.Identifier.Value}:{x.Key.Type.Name}"));
        }
    }
}