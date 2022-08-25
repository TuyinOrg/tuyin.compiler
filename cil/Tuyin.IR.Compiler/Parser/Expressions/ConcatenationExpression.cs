using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Expressions
{
    class ConcatenationExpression : RegularExpression 
    {
        internal override RegularExpressionType ExpressionType => RegularExpressionType.Concatenation;

        public List<RegularExpression> Expressions { get; }

        public ConcatenationExpression(RegularExpression left, RegularExpression right)
        {
            Expressions = new List<RegularExpression>();
            Expressions.Add(left);
            Expressions.Add(right);
        }

        public ConcatenationExpression(IEnumerable<RegularExpression> regexs) 
        {
            Expressions = new List<RegularExpression>(regexs);
        }

        public override string GetDescrption()
        {
            return string.Join(string.Empty, Expressions.Select(X => X.GetDescrption()));
        }

        internal override int GetMaxLength()
        {
            return Expressions.Sum(X => X.GetMaxLength());
        }

        internal override int GetMinLength()
        {
            return Expressions.Sum(X => X.GetMinLength());
        }

        internal override int RepeatLevel()
        {
            return Expressions.Sum(x => x.RepeatLevel());
        }

        internal override GraphEdgeStep<TMetadata> CreateGraphState<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> step, TMetadata metadata)
        {
            var expr = new GraphEdgeStep<TMetadata>[Expressions.Count];
            var curr = step;
            for (var i = 0; i < Expressions.Count; i++)
            {
                var exp = Expressions[i];
                curr = exp.CreateGraphState(figure, curr, metadata);
                expr[i] = curr;
            }

            return new NextStep<TMetadata>(expr[0].Start, expr[expr.Length - 1].End);
        }

        internal override string GetClearString()
        {
            var str = string.Empty;
            for (var i = 0; i < Expressions.Count; i++) 
            {
                var exp = Expressions[i];
                var expStr = exp.GetClearString();
                if (expStr == null)
                    return null;

                str = str + expStr;
            }

            return str;
        }
    }
}
