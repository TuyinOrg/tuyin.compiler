using libfsm;
using libgraph;
using System.Text;
using Tuyin.IR.Compiler.Parser.Generater;

namespace Tuyin.IR.Compiler.Parser
{
    static class DebugHelper
    {
        public static void SaveDebugGraphs(string fileName, params DebugGraph[] graphs)
        {
            var stream = File.Create(fileName);
            var bw = new BinaryWriter(stream, Encoding.UTF8);

            bw.Write(graphs.Length);
            for (var i = 0; i < graphs.Length; i++)
                graphs[i].Save(stream);

            bw.Flush();
            stream.Dispose();
        }

        public static IEnumerable<DebugGraph> FromGraphBox<TMetadata>(GraphBox<TMetadata> box) where TMetadata : struct
        {
            foreach (var figure in box.Figures)
                yield return FromGraphFigure(figure);
        }

        public static DebugGraph FromGraphFigure<TMetadata>(GraphFigure<TMetadata> figure) where TMetadata : struct
        {
            return FromGraph(figure.GraphBox, figure, figure.DisplayName);
        }

        private static DebugGraph FromGraph<TMetadata>(IGraph<GraphState<TMetadata>, GraphEdge<TMetadata>> box, IGraph<GraphState<TMetadata>, GraphEdge<TMetadata>> figure, string displayName)
        {
            var dict = new Dictionary<ushort, DebugVertex>();
            foreach (var vertex in box.Vertices)
                dict[vertex.Index] = new DebugVertex(vertex.Index, vertex.Descrption, VertexFlags.None);

            var result = new DebugGraph(displayName);
            foreach (var edge in figure.Edges)
                if (edge.Source != null)
                    result.AddEdge(new DebugEdge(EdgeFlags.None, edge.Descrption, dict[edge.Source.Index], dict[edge.Target.Index]));

            return result;
        }

        public static DebugGraph FromTransitions<T, TMetadata>(string graphName, IGraph<GraphState<TMetadata>, GraphEdge<TMetadata>> graph, IList<FATransition<T>> transitions, FAActionTable actions)
        {
            var states = graph.Vertices.OrderBy(x => x.Index).ToArray();
            var edges = graph.Edges.GroupBy(x => x.Source.Index).ToDictionary(x => x.Key, x => x.GroupBy(x => x.Target.Index).ToDictionary(y => y.Key, y => y.ToArray()));

            string GetEdgeDescrption(FATransition<T> tran)
            {
                var descrption = string.Empty;
                var symbols = new List<FASymbol>();
                if (tran.Symbol.Type == FASymbolType.Action)
                {
                    var action = actions[tran.Symbol.Value];
                    symbols.AddRange(action.Symbols);
                }
                else 
                {
                    symbols.Add(tran.Symbol);
                }

                if (symbols[0].Type.HasFlag(FASymbolType.Request))
                {
                    if (edges.ContainsKey(tran.SourceLeft))
                    {
                        if (edges[tran.SourceLeft].ContainsKey(tran.SourceRight))
                        {
                            descrption = edges[tran.SourceLeft][tran.SourceRight].Where(x => x.Contains((char)symbols[0].Value)).FirstOrDefault()?.Descrption ?? edges[tran.SourceLeft][tran.SourceRight].FirstOrDefault()?.Descrption;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(descrption))
                        descrption = tran.Input.ToString();
                    
                }
                else if (symbols[0].Type.HasFlag(FASymbolType.Report)) 
                {
                    descrption = tran.Input.ToString();
                    //descrption = edges[tran.SourceLeft][tran.SourceRight].Where(x => x.Contains(tran.Input)).First().Descrption;
                }
                else
                {
                    descrption = tran.Input.ToString();
                }

                if (descrption == "\0") 
                {
                    descrption = "ε";
                }

                string shift = string.Empty;
                for (var i = 0; i < symbols.Count; i++) 
                {
                    var sss = symbols[i];
                    var sd = sss.Value < states.Length ? states[sss.Value].Descrption : sss.Value.ToString();
                    if (string.IsNullOrWhiteSpace(sd))
                        sd = sss.Value.ToString();

                    switch (sss.Type)
                    {
                        case FASymbolType.Request:
                            shift = shift + "|" + $"⇝{sd}";
                            break;
                        case FASymbolType.Report:
                            shift = shift + "|" + $"{sd}↵";
                            break;
                        case FASymbolType.InGraph:
                            shift = shift + "|" + $"{sd}↺";
                            break;
                        case FASymbolType.Action:
                            throw new NotImplementedException();
                    }
                }

                if (!string.IsNullOrWhiteSpace(shift))
                    shift = $"({shift.Remove(0, 1)})";

                descrption = descrption + shift;
                return descrption;
            }

            var fileGraph = new DebugGraph(graphName);
            foreach (var group in transitions.GroupBy(x => new { Left = x.Left, Right = x.Right }))
            {
                string descrption = null;
                foreach (var tran in group)
                    descrption = descrption + GetEdgeDescrption(tran) + '\n';

                var edge = new DebugEdge(
                       EdgeFlags.None,
                       descrption.Substring(0, descrption.Length - 1),
                       new DebugVertex(group.Key.Left, group.Key.Left.ToString(), group.Key.Left == 0 ? VertexFlags.EndPoint : VertexFlags.None),
                       new DebugVertex(group.Key.Right, group.Key.Right.ToString(), group.Key.Right == 0 ? VertexFlags.EndPoint : VertexFlags.None));

                fileGraph.AddEdge(edge);
            }

            return fileGraph;
        }
    }
}
