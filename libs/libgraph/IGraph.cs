using System.Collections.Generic;
using System.Linq;

namespace libgraph
{
    public static class GraphExtands 
    {
        public static IEnumerable<TVertex> GetVertices<TVertex, TEdge>(this IGraph<TVertex, TEdge> libgraph)
                where TEdge : IEdge<TVertex>
                where TVertex : IVertex
        {
            return libgraph.Vertices;
        }
    }

    public interface IGraph<TVertex, TEdge>
        where TEdge : IEdge<TVertex> 
        where TVertex : IVertex
    {
        /// <summary>
        /// 获取顶点数量
        /// </summary>
        int VerticesCount => Vertices.Count();

        /// <summary>
        /// 获取顶点集合
        /// </summary>
        IEnumerable<TVertex> Vertices => Edges.Select(x => x.Target).Union(Edges.Select(x => x.Source)).Where(x => x != null).Distinct();

        /// <summary>
        /// 获取连接边
        /// </summary>
        IReadOnlyList<TEdge> Edges { get; }
    }
}
