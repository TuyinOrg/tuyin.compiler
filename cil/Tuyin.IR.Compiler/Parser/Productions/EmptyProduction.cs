using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Productions
{
    class EmptyProduction : ProductionBase
    {
        internal override ProductionType ProductionType => ProductionType.Empty;

        internal override string DebugNamePrefix => "E";

        public EmptyProduction()
        {
        }

        protected override GraphEdgeStep<TMetadata> InternalCreate<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry)
        {
            return entry;
        }

        internal override IEnumerable<ProductionBase> GetChildrens()
        {
            return new ProductionBase[0];
        }
    }
}
