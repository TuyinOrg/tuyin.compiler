using Tuyin.IR.Compiler.Parser;
using Tuyin.IR.Compiler.Parser.Generater;
using Tuyin.IR.Compiler.Target.Visitors;

namespace Tuyin.IR.Compiler.Target
{
    internal class SourceProduction : ProductionBase, IAstNode
    {
        internal override ProductionType ProductionType => Production.ProductionType;

        internal override string DebugNamePrefix => Production.DebugNamePrefix;

        public AstNodeType AstType => AstNodeType.Production;

        public SourceSpan SourceSpan { get; }

        public ProductionBase Production { get; }

        public int StartIndex => SourceSpan.StartIndex;

        public int EndIndex => SourceSpan.EndIndex;

        public T Visit<T>(AstVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public SourceProduction(SourceSpan sourceSpan, ProductionBase production)
        {
            SourceSpan = sourceSpan;
            Production = production;
        }

        protected override GraphEdgeStep<TMetadata> InternalCreate<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry)
        {
            return Production.Create(figure, last, entry);
        }

        internal override IEnumerable<ProductionBase> GetChildrens()
        {
            return Production.GetChildrens();
        }
    }
}
