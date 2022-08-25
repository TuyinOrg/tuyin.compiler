using System;
using System.Collections.Generic;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Analysis.Data.Instructions
{
    class ResetLabel : Statment
    {
        public ResetLabel(Label label)
        {
            Label = label;
        }

        public override AstNodeType NodeType => throw new NotImplementedException();

        public Label Label { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            yield return this;
        }
    }

}
