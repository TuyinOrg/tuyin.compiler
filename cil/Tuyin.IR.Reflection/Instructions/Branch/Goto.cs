using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    public class Goto : Statment
    {
        public Goto(Label target)
        {
            Label = target;
        }

        public override AstNodeType NodeType => AstNodeType.Goto;

        public Label Label { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            yield return this;
        }

        public override string ToString()
        {
            return $"goto {Label.Index}";
        }
    }
}
