using Tuyin.IR.Compiler.Parser.Generater;
using TMetadata = System.UInt16;

namespace Tuyin.IR.Compiler.Parser.Expressions
{
    class SymbolExpression : RegularExpression 
    {
        private string mClearString;

        internal override RegularExpressionType ExpressionType => RegularExpressionType.Symbol;

        public GraphEdgeValue Token { get; }

        public SymbolExpression(char c) 
        {
            Token = new GraphEdgeValue(c);
            mClearString = c.ToString();
        }

        protected override RegularExpression Combine(RegularExpression right)
        {
            if (right.ExpressionType == RegularExpressionType.CharSet)
                return new CharSetExpression(Token.Combine((right as CharSetExpression).Token));

            if (right.ExpressionType == RegularExpressionType.Symbol)
                return new CharSetExpression(Token.Combine((right as SymbolExpression).Token));

            return base.Combine(right);
        }

        public override string GetDescrption()
        {
            return Token.ToString();
        }

        internal override int GetMinLength()
        {
            return 1;
        }

        internal override int GetMaxLength()
        {
            return 1;
        }

        internal override int RepeatLevel()
        {
            return 0;
        }

        internal override GraphEdgeStep<TMetadata> CreateGraphState<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> step, TMetadata metadata)
        {
            var next = figure.State();
            for (var i = 0; i < step.End.Length; i++)
                figure.Edge(step.End[i], next, Token, metadata);

            return new NextStep<TMetadata>(step.End, next);
        }

        internal override string GetClearString()
        {
            return mClearString;
        }
    }
}
