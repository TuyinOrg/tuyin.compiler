using libgraph;

namespace BigBuns.Graph.Viewer
{
    static class GraphConverter
    {
        public static GraphExample Convert(DebugGraph graph)
        {
            var dict = new Dictionary<DebugVertex, DataVertex>();
            var pocGraph = new GraphExample();
            foreach (var vertex in graph.GetVertices())
            {
                var vvv = new DataVertex(vertex.Id, vertex.Tips);
                dict[vertex] = vvv;
                pocGraph.AddVertex(vvv);
            }

            foreach (var edge in graph.Edges) 
            {
                var pocEdge = new DataEdge(
                    dict[edge.Source],
                    dict[edge.Target],
                    2,
                    edge.Tips);

                pocEdge.Text = edge.Descrption;
                pocGraph.AddEdge(pocEdge);
            }

            return pocGraph;
        }
    }
}
