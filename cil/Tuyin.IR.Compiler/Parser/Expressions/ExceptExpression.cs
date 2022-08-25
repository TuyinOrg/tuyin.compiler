using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Expressions
{
    class ExceptExpression : RegularExpression
    {
        internal override RegularExpressionType ExpressionType => RegularExpressionType.Except;

        public GraphEdgeValue[] Tokens { get; }

        public ExceptExpression(string literal) 
            : this(literal.ToCharArray(), null)
        {
        }

        public ExceptExpression(string literal, IEnumerable<char> with) 
            : this(literal.ToCharArray(), with)
        {
        }

        public ExceptExpression(char[] chars, IEnumerable<char> with)
        {
            if (with != null)
            {
                Tokens = chars.Distinct().Select(x => new GraphEdgeValue(false, with.Concat(new char[] { x }).Distinct())).ToArray();
            }
            else 
            {
                Tokens = chars.Distinct().Select(x => new GraphEdgeValue(x)).ToArray();
            }
        }

        public override string GetDescrption()
        {
            return string.Join(string.Empty, Tokens.Select(X => X.ToString()));
        }

        internal override GraphEdgeStep<TMetadata> CreateGraphState<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> step, TMetadata metadata)
        {
            var edges = new List<GraphEdge<TMetadata>>();
            var last = step.End.ToArray();

            for (var i = 0; i < Tokens.Length; i++) 
            {
                var temp = new GraphState<TMetadata>[1];
                last.Do(x =>
                {
                    temp[0] = figure.State();
                    edges.Add(figure.Edge(x, temp[0], Tokens[i], metadata));
                });

                last = temp;
            }

            foreach (var edge in edges.Distinct().ToArray()) 
            {
                // 创建相反token
                var token = edge.Value.Reverse();

                // 连接到终点
                foreach (var end in step.End) 
                {
                    figure.Edge(edge.Source, end, token, edge.Metadata);
                }
            }

            return new NextStep<TMetadata>(step.End, last);
        }

        internal override int GetMaxLength()
        {
            return Tokens.Length;
        }

        internal override int GetMinLength()
        {
            return Tokens.Length;
        }

        internal override int RepeatLevel()
        {
            return Tokens.Length;
        }

        internal override string GetClearString()
        {
            var str = string.Empty;
            for (var i = 0; i < Tokens.Length; i++) 
            {
                var token = Tokens[i];
                var chars = token.GetChars(char.MaxValue);
                if (chars.OverCount(1))
                {
                    return null;
                }

                try
                {
                    str = str + chars.First().ToString();
                }
                catch
                {
                    return null;
                }
            }
            return str;
        }
    }
}
