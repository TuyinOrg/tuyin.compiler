using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Expressions
{
    class EmptyExpression : RegularExpression 
    {
        internal override RegularExpressionType ExpressionType => RegularExpressionType.Empty;

        public EmptyExpression() 
        {
        }

        public override string GetDescrption()
        {
            return string.Empty;
        }

        internal override int GetMaxLength()
        {
            return 0;
        }

        internal override int GetMinLength()
        {
            return 0;
        }

        internal override int RepeatLevel()
        {
            return 0;
        }

        internal override GraphEdgeStep<TMetadata> CreateGraphState<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> step, TMetadata metadata)
        {
            return step;
        }

        internal override string GetClearString()
        {
            return null;
        }
    }
}
