using Tuyin.IR.Compiler.Parser.Expressions;
using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Target
{
    internal class SourceExpression : RegularExpression, IAstNode
    {
        public SourceExpression(SourceSpan sourceSpan, RegularExpression regex)
        {
            SourceSpan = sourceSpan;
            Regex = regex;
        }

        public SourceSpan SourceSpan { get; }

        public RegularExpression Regex { get; }

        public int StartIndex => SourceSpan.StartIndex;

        public int EndIndex => SourceSpan.EndIndex;

        public AstNodeType AstType => AstNodeType.Regex;

        internal override RegularExpressionType ExpressionType => Regex.ExpressionType;

        public override string GetDescrption()
        {
            return Regex.GetDescrption();
        }

        public T Visit<T>(Visitors.AstVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        internal override GraphEdgeStep<TMetadata> CreateGraphState<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> step, TMetadata metadata)
        {
            return Regex.CreateGraphState(figure, step, metadata);
        }

        internal override string GetClearString()
        {
            return Regex.GetClearString();
        }

        internal override int GetMaxLength()
        {
            return Regex.GetMaxLength();
        }

        internal override int GetMinLength()
        {
            return Regex.GetMinLength();
        }

        internal override int RepeatLevel()
        {
            return Regex.RepeatLevel();
        }
    }
}