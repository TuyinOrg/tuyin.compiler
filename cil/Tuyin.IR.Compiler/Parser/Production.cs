using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser
{
    interface IProduction
    {
        bool HasRecursive { get; }

        void FixRecursive();
    }

    class Production : ProductionBase, IProduction
    {
        enum CreateState
        {
            NoGraph,
            Createing,
            Created
        }

        private bool mInFixLoop;
        private bool mInFixLoop2;
        private bool mInChildrenLoop;
        private bool mHasRecursive;
        private CreateState mState;
        private object mEntry;
        private ProductionBase mRule;

        private string mProductionName;

        internal override ProductionType ProductionType => ProductionType.Recursive;

        internal override string DebugNamePrefix => mProductionName;

        public bool HasRecursive 
        {
            get { return mHasRecursive; }
        }

        public ProductionBase Rule
        {
            get { return mRule; }
            set
            {
                if (mRule != value)
                {
                    mRule = value;
                    mHasRecursive = value.Scan(null, p => p == this).Count() > 0;
                }
            }
        }

        public Production(string productionName) 
        {
            mProductionName = productionName;
        }

        public void FixRecursive()
        {
            mInFixLoop = false;
            RepairRecursive(this);
        }

        protected override GraphEdgeStep<TMetadata> InternalCreate<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry) where TMetadata : struct
        {
            if (mHasRecursive)
            {
            
                if (mState == CreateState.NoGraph)
                {
                    var newFigure = figure.GraphBox.Figure(mProductionName);
                    mEntry = newFigure;

                    mState = CreateState.Createing;
                    Rule.Create(newFigure, last, new Entry<TMetadata>(newFigure));
                    mState = CreateState.Created;
                }

                return ConnectToWarp(figure, last, entry);
            }
            else
            {
                return Rule.Create(figure, last, entry);
            }
        }

        private GraphEdgeStep<TMetadata> ConnectToWarp<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry) where TMetadata : struct
        {
            var end = figure.State();
            var entryFigure = mEntry as GraphFigure<TMetadata>;
            foreach (var start in entry.End)
            {
                var edge = figure.Edge(start, end, new GraphEdgeValue(entryFigure.Index), default);
                edge.Subset = entryFigure;
                edge.Descrption = entryFigure.DisplayName;
            }

            //if(mState == CreateState.Createing)
            //     return new NextStep<TMetadata>(entry.End, new GraphState<TMetadata>[0]);

            return new NextStep<TMetadata>(entry.End, end);
        }

        internal override ProductionBase RepairInvalid()
        {
            if (!mInFixLoop2)
            {
                mInFixLoop2 = true;
                Rule = Rule.RepairInvalid();
            }
            return this;
        }

        protected override ProductionBase InternalRepairRecursive(IProduction parser)
        {
            if (!mInFixLoop)
            {
                mInFixLoop = true;

                Rule = Rule.RepairRecursive(this);
                if (parser != this && parser != null)
                    Rule = Rule.RepairRecursive(parser);
            }

            return this;
        }

        internal override IEnumerable<ProductionBase> GetChildrens()
        {
            if (!mInChildrenLoop)
            {
                mInChildrenLoop = true;
                yield return Rule;
                mInChildrenLoop = false;
            }
        }
    }
}
