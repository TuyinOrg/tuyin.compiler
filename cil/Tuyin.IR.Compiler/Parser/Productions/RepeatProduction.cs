using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Productions
{
    class RepeatProduction : ProductionBase
    {
        internal override ProductionType ProductionType => ProductionType.Repeat;

        internal override string DebugNamePrefix => "R{0}";

        private ProductionBase production;
        private ProductionBase separator;

        public RepeatProduction(ProductionBase production, ProductionBase separator)
        {
            this.production = production;
            this.separator = separator;
            this.production.Parent = this;
         
            if (separator != null)
                this.separator.Parent = this;
        }

        internal override ProductionBase RepairInvalid()
        {
            if (separator != null)
            {
                return new RepeatProduction(production.RepairInvalid(), separator.RepairInvalid());
            }
            else
            {
                return new RepeatProduction(production.RepairInvalid(), null);
            }
        }

        protected override ProductionBase InternalRepairRecursive(IProduction parser)
        {
            production = production.RepairRecursive(parser);
            if (separator != null)
                separator = separator.RepairRecursive(parser);

            return this;
        }

        protected override GraphEdgeStep<TMetadata> InternalCreate<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry)
        {
            var heads = new HashSet<ushort>();
            foreach (var start in entry.End)
                heads.Add(start.Index);

            figure.GraphBox.StartCollect();
            var step = this.production.Create(figure, last, entry);

            if (this.separator != null)
                step = this.separator.Create(figure, entry, step);

            var newEdges = figure.GraphBox.EndCollect();

            foreach (var right in newEdges.Where(x => heads.Contains(x.Source.Index)))
                foreach (var end in step.End)
                    figure.Edge(end, right.Target, right.Value, right.Metadata).Descrption = right.Descrption;

            return new NextStep<TMetadata>(step.Start, entry.End);
        }

        internal override IEnumerable<ProductionBase> GetChildrens()
        {
            yield return production;
            if (separator != null)
                yield return separator;
        }
    }
}
