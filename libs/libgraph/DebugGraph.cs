using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace libgraph
{
    public class DebugGraph : IGraph<DebugVertex, DebugEdge>
    {
        private List<DebugEdge> mEdges;

        public IReadOnlyList<DebugEdge> Edges => mEdges;

        public string GraphName { get; }

        public DebugGraph(string graphName)
        {
            GraphName = graphName;
            mEdges = new List<DebugEdge>();
        }

        public void AddEdge(DebugEdge edge) 
        {
            mEdges.Add(edge);
        }

        public void Save(string fileName)
        {
            Save(File.Create(fileName));
        }

        public void Save(Stream stream) 
        {
            var bw = new BinaryWriter(stream);
            var verties = Edges.Select(x => x.Target).Union(Edges.Select(x => x.Source)).Distinct().ToArray();
            bw.Write(GraphName);
            bw.Write(verties.Length);
            foreach (var vert in verties)
            {
                bw.Write(vert.Index);
                bw.Write(vert.Id);
                bw.Write((ushort)vert.Flags);
                bw.Write(vert.Tips ?? String.Empty);
            }

            bw.Write(Edges.Count);
            foreach (var edge in Edges)
            {
                bw.Write((ushort)edge.Flags);
                bw.Write(edge.Descrption ?? String.Empty);
                bw.Write(edge.Tips ?? String.Empty);
                bw.Write(edge.Source.Index);
                bw.Write(edge.Target.Index);
            }

            bw.Flush();
        }

        public static DebugGraph Load(string fileName)
        {
            return Load(File.OpenRead(fileName));
        }

        public static DebugGraph Load(Stream stream)
        {
            var br = new BinaryReader(stream);
            var dict = new Dictionary<int, DebugVertex>();

            var libgraph = new DebugGraph(br.ReadString());

            var vertCount = br.ReadInt32();
            for (var i = 0; i < vertCount; i++)
            {
                var index = br.ReadUInt16();
                dict[index] = new DebugVertex(index, br.ReadString(), (VertexFlags)br.ReadUInt16(), br.ReadString());
            }

            var edgeCount = br.ReadInt32();
            for (var i = 0; i < edgeCount; i++)
            {
                var flags = (EdgeFlags)br.ReadUInt16();
                var descrption = br.ReadString();
                var tips = br.ReadString();
                var srcIndex = br.ReadUInt16();
                var tarIndex = br.ReadUInt16();

                libgraph.AddEdge(new DebugEdge(
                    flags,
                    descrption,
                    dict[srcIndex],
                    dict[tarIndex],
                    tips));
            }

            return libgraph;
        }
    }
}
