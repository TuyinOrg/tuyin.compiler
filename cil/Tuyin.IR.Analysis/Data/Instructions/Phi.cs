using System;
using System.Collections.Generic;
using System.Linq;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Analysis.Data.Instructions
{
    class Phi : Expression
    {
        public IReadOnlyList<Expression> Eexpressions { get; }

        public Phi(IReadOnlyList<Expression> expressions)
        {
            Eexpressions = expressions;
        }

        public override AstNodeType NodeType => AstNodeType.Custom;

        public override string ToString()
        {
            return $"Φ({string.Join(",", Eexpressions.Select(x => x.ToString()))})";
        }

        public override IEnumerable<AstNode> GetNodes()
        {
            yield return this;
        }
    }
}
