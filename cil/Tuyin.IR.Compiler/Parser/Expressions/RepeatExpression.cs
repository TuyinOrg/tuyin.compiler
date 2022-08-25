using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser.Expressions
{
    class RepeatExpression : RegularExpression 
    {
        internal override RegularExpressionType ExpressionType => RegularExpressionType.Repeat;

        public RegularExpression Expression { get; }

        public RepeatExpression(RegularExpression exp) 
        {
            Expression = exp;
        }

        public override string GetDescrption()
        {
            return $"{Expression.GetDescrption()}*";
        }

        internal override int GetMinLength()
        {
            return 0;
        }

        internal override int GetMaxLength()
        {
            return Expression.GetMaxLength();
        }

        internal override int RepeatLevel()
        {
            return Expression.RepeatLevel() + 1;
        }

        internal override GraphEdgeStep<TMetadata> CreateGraphState<TMetadata>(GraphFigure<TMetadata> figure, GraphEdgeStep<TMetadata> step, TMetadata metadata)
        {
            /*
            graph.StartPath();
            var first = Expression.CreateGraphState(graph, terminal, vertices);
            var path = graph.EndPath();

            var ends = new HashSet<NFAState>();
            Helper.Foreach(first.Start.SelectMany2(x => x.Right).Where(x => path.Contains(x)).ToArray(), first.End, (start, end) => 
            {
                var right = start.Right;
                if (end == right) 
                {
                    end = start.Left;
                    right = start.Left;

                    graph.MarkRemove(start);
                }

                if (right == graph.Main)
                {
                    var newEdge = graph.Edge(end, start.Value, start.Token, start.Flags);
                    newEdge.Descrption = start.Descrption;
                    end = newEdge.Right;
                    right = newEdge.Right;
                }

                var edge = graph.Edge(end, right, start.Value, start.Token, start.Flags | NFAEdgeFlags.Loop);
                edge.Descrption = start.Descrption;
                ends.Add(edge.Right);
            });

            return new NextStep(vertices.End, ends);
            */

            var newGraph = figure.GraphBox.TemporaryFigure();// new GraphFigure(true, graph.Lexicon, graph.SkipTokens);
            var first = Expression.CreateGraphState(newGraph, new Entry<TMetadata>(newGraph.Main), metadata);
            var ends = new HashSet<GraphState<TMetadata>>();

            /*
            Helper.Foreach(first.Start.SelectMany2(x => x.Right), first.End, (s, e) =>
            {
                var edge = newGraph.Edge(e, s.Left, s.Value, s.Token, s.Flags | DFAEdgeFlags.Loop);
                edge.Descrption = s.Descrption;
                ends.Add(edge.Right);
                

            });
            */

            foreach (var edge in first.End.SelectMany(x => figure.GetLefts(x)).ToArray()) 
            {
                foreach (var s in first.Start)
                {
                    var nedge = newGraph.Edge(edge.Source, s, edge.Value, edge.Metadata);
                    nedge.Descrption = edge.Descrption;
                    ends.Add(s);
                }

                newGraph.Remove(edge);
            }

            var rends = new List<GraphState<TMetadata>>();
            var mapping = new Dictionary<GraphState<TMetadata>, GraphState<TMetadata>>();
            mapping[newGraph.Exit] = figure.Exit;
            foreach (var edge in newGraph.Edges)
            {
                var isEnd = ends.Contains(edge.Target);
                var lefts = edge.Source == newGraph.Main ? step.End : GetTargets(figure, mapping, edge.Source);
                var rights = edge.Target == newGraph.Main ? step.End : GetTargets(figure, mapping, edge.Target);

                foreach (var left in lefts) 
                {
                    foreach (var right in rights) 
                    {
                        if (right == figure.Main)
                        {
                            var newState = figure.State();
                            figure.Edge(left, newState, edge.Value, edge.Metadata).Descrption = edge.Descrption;
                            figure.Edge(newState, newState, edge.Value, edge.Metadata).Descrption = edge.Descrption;

                            if (isEnd)
                            {
                                rends.Add(newState);
                            }
                        }
                        else
                        {

                            var redge = figure.Edge(left, right, edge.Value, edge.Metadata);
                            redge.Descrption = edge.Descrption;

                            if (isEnd)
                            {
                                rends.Add(redge.Source);
                            }
                        }
                    }
                }
            }

            var start = first.Start.Where(x => x == newGraph.Main).SelectMany(x => step.End).Concat(first.Start.Where(x => x != newGraph.Main).Select(x => mapping[x])).Distinct().ToArray();
            //var vend  = first.End.Where(x => x == newGraph.Main).SelectMany2(x => vertices.End).Concat(first.End.Where(x => x != newGraph.Main).Select(x => mapping[x])).Distinct().ToArray();
            return new NextStep<TMetadata>(start, rends);
            
        }

        private GraphState<TMetadata>[] GetTargets<TMetadata>(GraphFigure<TMetadata> figure, Dictionary<GraphState<TMetadata>, GraphState<TMetadata>> mapping, GraphState<TMetadata> target)
            where TMetadata : struct
        {
            if (!mapping.ContainsKey(target)) mapping[target] = figure.State();

            return new GraphState<TMetadata>[] { mapping[target] };
        }

        internal override string GetClearString()
        {
            return null;
        }
    }
}
