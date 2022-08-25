using System.Collections.Generic;

namespace Tuyin.IR.Reflection.Instructions
{
    public class Test : Goto
    {

        public Test(Label label, Expression exp)
            : base(label)
        {
            Expression = exp;
        }

        public override AstNodeType NodeType => AstNodeType.Test;

        public Expression Expression { get; }

        public override IEnumerable<AstNode> GetNodes()
        {
            if (Expression != null)
                foreach (var item in Expression.GetNodes())
                    yield return item;

            yield return this;
        }

        public override string ToString()
        {
            return $"test {Expression}:{Label.Index}";
        }
    }
}
