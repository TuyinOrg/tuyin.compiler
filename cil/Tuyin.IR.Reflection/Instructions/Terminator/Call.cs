using System.Collections.Generic;
using System.Linq;

namespace Tuyin.IR.Reflection.Instructions
{
    public class Call : Expression
    {
        public Call(Expression method, IEnumerable<Expression> args)
        {
            Method = method;
            Arguments = args.ToArray();
        }

        public override AstNodeType NodeType => AstNodeType.Call;

        public Expression Method { get; }

        public IReadOnlyList<Expression> Arguments { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            foreach(var arg in Arguments)
                foreach(var node in arg.GetNodes())
                    yield return node;

            foreach (var node in Method.GetNodes())
                yield return node;

            yield return this;
        }

        public override string ToString()
        {
            return $"call {Method}({string.Join(",", Arguments.Select(x => x.ToString()))})";
        }
    }
}