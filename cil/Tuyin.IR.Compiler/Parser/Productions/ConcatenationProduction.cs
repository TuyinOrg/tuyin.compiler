using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Productions
{
    class ConcatenationProduction : ProductionBase
    {
        internal override ProductionType ProductionType => ProductionType.Concatenation;

        internal override string DebugNamePrefix => "C{0}";

        private ProductionBase production;
        private ProductionBase production2;

        public ConcatenationProduction(ProductionBase production, ProductionBase production2)
        {
            this.production = production;
            this.production2 = production2;

            this.production.Parent = this;
            this.production2.Parent = this;
        }

        protected override GraphEdgeStep<TMetadata> InternalCreate<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry)
        {
            var first = production.Create(figure, last, entry);
            var follow = production2.Create(figure, entry, first);
            return new NextStep<TMetadata>(first.Start, follow.End);
        } 

        internal override ProductionBase RepairInvalid()
        {
            if (production.ProductionType == ProductionType.Empty)
                return production2.RepairInvalid();

            if (production2.ProductionType == ProductionType.Empty)
                return production.RepairInvalid();

            if (production.ProductionType == ProductionType.Repeat)
                if (production.GetChildrens().First() == production2)
                    return production.RepairInvalid();

            return new ConcatenationProduction(production.RepairInvalid(), production2.RepairInvalid());
        }

        protected override ProductionBase InternalRepairRecursive(IProduction parser)
        {
            var post = production2.RepairRecursive(null);
            var pre = production.RepairRecursive(parser);

            if (pre.Equals(parser))
                return post;
            else if (pre.Equals(production) && post.Equals(production2))
                return this;

            return new ConcatenationProduction(pre, post);
        }

        internal override IEnumerable<ProductionBase> GetChildrens()
        {
            yield return production;
            yield return production2;
        }
    }
}
