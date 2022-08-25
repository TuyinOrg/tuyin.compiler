namespace Tuyin.IR.Compiler.Parser.Generater
{
    interface GraphEdgeStep<TMetadata>
    {
        GraphState<TMetadata>[] Start { get; }

        GraphState<TMetadata>[] End { get; }

        bool IsStartup => Start[0] == End[0];
    }

    abstract class BaseStep<TMetadata> : GraphEdgeStep<TMetadata>
    {
        public static readonly NextStep<TMetadata> EMPTY = new NextStep<TMetadata>(new GraphState<TMetadata>[0], new GraphState<TMetadata>[0]);

        public abstract GraphState<TMetadata>[] Start { get; }

        public abstract GraphState<TMetadata>[] End { get; }
    }

    class Entry<TMetadata> : BaseStep<TMetadata>
    {
        public override GraphState<TMetadata>[] Start { get; }

        public override GraphState<TMetadata>[] End => Start;

        internal Entry(GraphState<TMetadata> main)
        {
            Start = new GraphState<TMetadata>[1] { main };
        }
    }

    class NextStep<TMetadata> : BaseStep<TMetadata>
    {
        public override GraphState<TMetadata>[] Start { get; }

        public override GraphState<TMetadata>[] End { get; }

        internal NextStep(GraphEdgeStep<TMetadata> left, GraphEdgeStep<TMetadata> right)
        {
            Start = left.Start;
            End = right.End;
        }

        internal NextStep(IEnumerable<GraphState<TMetadata>> left, IEnumerable<GraphState<TMetadata>> right)
        {
            Start = left.Distinct().ToArray();
            End = right.Distinct().ToArray();
        }

        internal NextStep(IEnumerable<GraphState<TMetadata>> left, GraphState<TMetadata> right)
        {
            Start = left.Distinct().ToArray();
            End = new GraphState<TMetadata>[] { right };
        }
    }

    class ConnectStep<TMetadata> : BaseStep<TMetadata>
    {
        public override GraphState<TMetadata>[] Start { get; }

        public override GraphState<TMetadata>[] End { get; }

        internal ConnectStep(IEnumerable<GraphEdgeStep<TMetadata>> results)
        {
            Start = results.SelectMany(x => x.Start).Distinct().ToArray();
            End = results.SelectMany(x => x.End).Distinct().ToArray();
        }

        internal ConnectStep(IEnumerable<GraphEdgeStep<TMetadata>> results, IEnumerable<GraphState<TMetadata>> otherEnds)
        {
            Start = results.SelectMany(x => x.Start).Distinct().ToArray();
            End = results.SelectMany(x => x.End).Concat(otherEnds).Distinct().ToArray();
        }
    }
}
