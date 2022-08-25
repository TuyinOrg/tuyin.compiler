using System.Collections.Generic;
using System.Diagnostics;

namespace Tuyin.IR.Reflection
{
    [DebuggerDisplay("{'[' + SourceSpan.ToString() + ']' + ToString()}")]
    public abstract class AstNode
    {
        public SourceSpan SourceSpan { get; set; }

        public abstract AstNodeType NodeType { get; }

        public abstract IEnumerable<AstNode> GetNodes();
    }
}
