using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Expressions
{
    class CharSetExpression : RegularExpression 
    {
        internal override RegularExpressionType ExpressionType => RegularExpressionType.CharSet;

        public GraphEdgeValue Token { get; }

        public CharSetExpression(bool xor, params char[] chars)
        {
            Token = new GraphEdgeValue(xor, chars);
        }

        public CharSetExpression(params char[] chars) 
            : this(false, chars)
        {
        }

        internal CharSetExpression(GraphEdgeValue vector) 
        {
            Token = vector;
        }

        protected override RegularExpression Combine(RegularExpression right)
        {
            if (right.ExpressionType == RegularExpressionType.CharSet)
                return new CharSetExpression(Token.Combine((right as CharSetExpression).Token));

            if(right.ExpressionType == RegularExpressionType.Symbol)
                return new CharSetExpression(Token.Combine((right as SymbolExpression).Token));

            return base.Combine(right);
        }

        internal override int GetMaxLength()
        {
            return 1;
        }

        internal override int GetMinLength()
        {
            return 1;
        }

        internal override int RepeatLevel()
        {
            return 0;
        }

        public override string GetDescrption()
        {
            return Token.ToString();
        }

        internal override string GetClearString()
        {
            var chars = Token.GetChars(char.MaxValue);
            if (chars.OverCount(1)) 
            {
                return null;
            }

            try
            {
                return chars.First().ToString();
            }
            catch 
            {
                return null;
            }
        }

        internal override GraphEdgeStep<TMetadata> CreateGraphState<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> step, TMetadata metadata)
        {
            var next = figure.State();
            for (var i = 0; i < step.End.Length; i++)
                figure.Edge(step.End[i], next, Token, metadata);

            return new NextStep<TMetadata>(step.End, next);
        }
    }
}
