using libgraph;
using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Productions
{
    class OrProduction : ProductionBase
    {
        private bool mHasEmpty;

        internal override string DebugNamePrefix => "O{0}";

        internal override ProductionType ProductionType => ProductionType.Or;

        public ProductionBase[] Forks { get; private set; }

        public OrProduction(IEnumerable<ProductionBase> forks)
        {
            Forks = Init(forks.ToList());
        }

        public OrProduction(ProductionBase left, ProductionBase right)
        {
            Forks = Init(Simplify(left, right));
        }

        private ProductionBase[] Init(List<ProductionBase> forks)
        {
            var empties = forks.Where(X => X.ProductionType == ProductionType.Empty).ToArray();
            forks.RemoveAll(x => empties.Contains(x));
            foreach (var fork in forks)
                fork.Parent = this;

            mHasEmpty = empties.Length > 0;
            if (mHasEmpty)
            {
                var empty = empties.FirstOrDefault();
                if (empty != null)
                {
                    forks.Insert(0, empty);
                    empty.Parent = this;
                }
            }
    
            return forks.ToArray();
        }

        private List<ProductionBase> Simplify(params ProductionBase[] productions)
        {
            var prods = new List<ProductionBase>();

            for (var i = 0; i < productions.Length; i++)
            {
                var prod = productions[i];
                if (prod.ProductionType == ProductionType.Or)
                {
                    prods.AddRange((prod as OrProduction).Forks);
                }
                else
                {
                    prods.Add(prod);
                }
            }

            return prods;
        }

        protected override GraphEdgeStep<TMetadata> InternalCreate<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> last, GraphEdgeStep<TMetadata> entry)
        {
            if (mHasEmpty)
            {
                var heads = new HashSet<ushort>();
                foreach (var start in entry.End)
                    heads.Add(start.Index);

                figure.GraphBox.StartCollect();

                var results = new GraphEdgeStep<TMetadata>[Forks.Length];
                for (int i = 0; i < Forks.Length; i++)
                    results[i] = Forks[i].Create(figure, last, entry);

                var newEdges = figure.GraphBox.EndCollect();
                foreach (var right in newEdges.Where(x => heads.Contains(x.Source.Index)))
                    right.Flags = right.Flags | EdgeFlags.Optional;

                return new NextStep<TMetadata>(entry.End, results.SelectMany(x => x.End));
            }
            else 
            {
                var results = new GraphEdgeStep<TMetadata>[Forks.Length];
                for (int i = 0; i < Forks.Length; i++)
                    results[i] = Forks[i].Create(figure, last, entry);

                return new NextStep<TMetadata>(entry.End, results.SelectMany(x => x.End));
            }
        }

        protected override ProductionBase InternalRepairRecursive(IProduction parser)
        {
            var second = new List<ProductionBase>();
            var first = new List<ProductionBase>(Forks);
            for (var i = 0; i < first.Count; i++)
            {
                var item = first[i];
                var itemexp = item.ProductionType == ProductionType.Recursive ? (item as Production).Rule : item;
                var fix = item.RepairRecursive(parser);
                var fixexp = fix.ProductionType == ProductionType.Recursive ? (fix as Production).Rule : fix;

                if (!itemexp.Equals(fixexp))
                {
                    second.Add(item);
                    first.RemoveAt(i);
                    i--;
                }
            }

            var result = first.Count > 1 ? new OrProduction(first) : first[0];
            if (second.Count > 0)
                result = new ConcatenationProduction(result, new RepeatProduction(second.Count > 1 ? new OrProduction(second) : second[0], null));

            return result;
        }

        internal override ProductionBase RepairInvalid()
        {
            var forks = new List<ProductionBase>(Forks.Length);
            for (var i = 0; i < Forks.Length; i++)
            {
                var fork = Forks[i];
                fork = fork.RepairInvalid();
                if (fork.ProductionType == ProductionType.Concatenation)
                {
                    var childrens = fork.GetChildrens().ToArray();
                    if (childrens[1].ProductionType == ProductionType.Repeat && childrens[0] == childrens[1].GetChildrens().First())
                    {
                        forks[i] = childrens[1];
                    }
                }
                else if (fork.ProductionType == ProductionType.Recursive)
                {
                    var real = fork.GetChildrens().First();
                    if (real.ProductionType == ProductionType.Concatenation)
                    {
                        var childrens = real.GetChildrens().ToArray();
                        if (childrens[1].ProductionType == ProductionType.Repeat && childrens[0] == childrens[1].GetChildrens().First())
                        {
                            (Forks[i] as Production).Rule = childrens[1];
                            forks[i] = Forks[i];
                        }
                    }
                }
            }

            return new OrProduction(forks);
        }

        internal override IEnumerable<ProductionBase> GetChildrens()
        {
            return Forks;
        }
    }
}
