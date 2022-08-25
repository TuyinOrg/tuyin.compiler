using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Expressions
{
    class OrExpression : RegularExpression
    {
        private bool mHasEmpty;

        internal override RegularExpressionType ExpressionType => RegularExpressionType.Or;

        public List<RegularExpression> Forks { get; }

        public OrExpression(RegularExpression left, RegularExpression right)
        {
            Forks = Simplify(left, right);

            var empties = Forks.Where(X => X.ExpressionType == RegularExpressionType.Empty).ToArray();
            Forks.RemoveAll(X => empties.Contains(X));

            mHasEmpty = empties.Length > 0;
            if (mHasEmpty)
            {
                var empty = empties.FirstOrDefault();
                if (empty != null)
                {
                    Forks.Add(empty);
                }
            }
        }

        public override string GetDescrption()
        {
            return string.Join("|", Forks.Select(X => X.GetDescrption()));
        }

        internal override int GetMaxLength()
        {
            return Forks.Max(X => X.GetMaxLength());
        }

        internal override int GetMinLength()
        {
            return Forks.Max(X => X.GetMinLength());
        }

        internal override string GetClearString()
        {
            return null;
        }

        internal override int RepeatLevel()
        {
            return Forks.Max(x => x.RepeatLevel());
        }

        internal override GraphEdgeStep<TMetadata> CreateGraphState<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> step, TMetadata metadata)
        {
            return new ConnectStep<TMetadata>(Forks.Select(X => X.CreateGraphState(figure, step, metadata)).ToArray());
        }

        private List<RegularExpression> Simplify(params RegularExpression[] exps)
        {
            var prods = new List<RegularExpression>();
            for (var i = 0; i < exps.Length; i++)
            {
                var prod = exps[i];
                if (prod.ExpressionType == RegularExpressionType.Or)
                {
                    prods.AddRange((prod as OrExpression).Forks);
                }
                else
                {
                    prods.Add(prod);
                }
            }

            return prods;
        }
    }
}
