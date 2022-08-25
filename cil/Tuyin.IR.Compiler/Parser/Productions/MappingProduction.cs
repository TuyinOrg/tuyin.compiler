using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Productions
{
    class MappingProduction : ProductionBase
    {
        internal override ProductionType ProductionType => ProductionType.Mapping;

        internal override string DebugNamePrefix => "M{0}";

        private ProductionBase production;
        private string outpit;

        public MappingProduction(ProductionBase production, string output)
        {
            this.outpit = output;
            this.production = production;
            this.production.Parent = this;
        }

        protected override GraphEdgeStep<TMetadata> InternalCreate<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry)
        {
            return production.Create(figure, last, entry);
        }

        internal override ProductionBase RepairInvalid()
        {
            return new MappingProduction(production.RepairInvalid(), this.outpit);
        }

        protected override ProductionBase InternalRepairRecursive(IProduction parser)
        {
            var first = production.RepairRecursive(parser);
            if (first.Equals(production))
                return this;

            return new MappingProduction(first, this.outpit);
        }

        internal override IEnumerable<ProductionBase> GetChildrens()
        {
            yield return production;
        }
    }
}
