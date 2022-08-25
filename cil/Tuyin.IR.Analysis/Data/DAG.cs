using GiGraph.Dot.Entities.Graphs;
using GiGraph.Dot.Entities.Html.Table;
using GiGraph.Dot.Extensions;
using GiGraph.Dot.Types.Colors;
using GiGraph.Dot.Types.Edges;
using GiGraph.Dot.Types.Fonts;
using GiGraph.Dot.Types.Styling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tuyin.IR.Analysis.Data.Instructions;
using Tuyin.IR.Analysis.Passes;
using Tuyin.IR.Analysis.Utils.Colors;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Analysis.Data
{
    public class DAG : AnalysisGraphBase<DAGNode>
    {
        internal DAG(Metadatas metadatas, IReadOnlyList<AnalysisEdge> edges, IReadOnlyList<DAGNode> vertexs)
        {
            Metadatas = metadatas;
            Edges = edges;
            Vertices = vertexs;
        }

        public Metadatas Metadatas { get; }

        public DAGNode Entry => Edges[0].Source as DAGNode;

        public override IReadOnlyList<AnalysisEdge> Edges { get; }

        public override IReadOnlyList<DAGNode> Vertices { get; }

        public override void SaveToFile(string fileName)
        {
            var type = new TypeAnalysis(this, Metadatas);
            var graph = this as IAnalysisGraph<DAGNode>;
            var dot = new DotGraph(directed: true);
            var nodes = new Dictionary<string, DotNodeBuilder>();

            void CreateVertex(DAGNode node)
            {
                if (!nodes.ContainsKey(node.Name.Identifier))
                    nodes.Add(node.Name.Value, new DotNodeBuilder(node, node.Name.Value));

                node.BuildDot(nodes[node.Name.Value], Metadatas, 0, 0);
            }

            void CreateEdge(AnalysisEdge edge) 
            {
                var src = edge.Source as DAGNode;
                var dst = edge.Target as DAGNode;

                var sad = DotNodeBuilderHelper.FindRoot(src.Name);
                var dad = DotNodeBuilderHelper.FindRoot(dst.Name);

                var spt = DotNodeBuilderHelper.FindPort(src.Name);
                var dpt = DotNodeBuilderHelper.FindPort(dst.Name);

                nodes[sad].Edges.Add(edge);

                Color[] colors = null;
                if (dst is DAGStoreNode store)
                {
                    if (edge.Subset == null)
                        throw new System.NotImplementedException();

                    var load = edge.Subset as DAGLoadNode;
                    colors = new Color[load.Fields.Length];
                    for (var i = 0; i < load.Fields.Length; i++) 
                    {
                        var field = load.Fields[i];
                        // 染色
                        var row = field.Name.UseIndex + 1;
                        var column = store.Childrens.Columns[field.Name.Identifier].Column + 1;

                        if (!store.Childrens.Colors.ContainsKey(row, column))
                            store.Childrens.Colors[row, column] = RandomColor.Get(
                                EColorScheme.Monochrome, 
                                ELuminosity.Bright);

                        colors[i] = store.Childrens.Colors[row, column];
                    }
                }

                var dotEdge = dot.Edges.Add(sad, dad);
                if (!string.IsNullOrEmpty(spt))
                    dotEdge.Tail.Endpoint.Port = new DotEndpointPort(spt, DotCompassPoint.Default);
                else
                    dotEdge.Tail.Endpoint.Port = new DotEndpointPort((nodes[sad].Edges.Count - 1).ToString(), DotCompassPoint.Default);

                if (!string.IsNullOrEmpty(dpt))
                    dotEdge.Head.Endpoint.Port = new DotEndpointPort(dpt, DotCompassPoint.Default);

                if (colors != null)
                {
                    dotEdge.Color = new DotMultiColor(colors);
                    dotEdge.FillColor = Color.Black;
                }
            }

            void BuildVertex(DotNodeBuilder builder) 
            {
                var store = builder.Root as DAGStoreNode;
                if (store != null && builder.Cells.Count > 0)
                {
                    for (var i = 0; i < builder.Edges.Count; i++)
                    {
                        var edge = builder.Edges[i];
                        var src = edge.Source as DAGNode;
                        if (store.Childrens.Columns.ContainsKey(src.Name.Identifier))
                        {
                            var row = src.Name.UseIndex + 1;
                            var column = store.Childrens.Columns[src.Name.Identifier].Column + 1;
                            builder.Cells.Add(new DotNodeBuilder.Cell(row, column, type.DeducedType(builder.Edges[i]).Name, DotNodeBuilderHelper.FindPort(src.Name)));
                        }
                        else if (builder.Cells.Count == 1)
                        {
                            builder.Cells.Add(new DotNodeBuilder.Cell(builder.Cells[0].Row + 1, builder.Cells[0].Column, type.DeducedType(builder.Edges[i]).Name, i.ToString()));
                        }
                        else throw new NotImplementedException();
                    }
                }
                else
                {
                    for (var i = 0; i < builder.Edges.Count; i++)
                    {
                        builder.Cells.Add(new DotNodeBuilder.Cell(builder.Cells[0].Row + 1, builder.Cells[0].Column, type.DeducedType(builder.Edges[i]).Name, i.ToString()));
                    }
                }

                var rows = builder.Cells.GroupBy(x => x.Row).Select(x => new { Row = x.Key, Value = x.ToArray() }).OrderBy(x => x.Row).Select(x => x.Value).ToArray();
                var table = new DotHtmlTable
                {
                    BorderWidth = 1,
                    CellBorderWidth = 0,
                    CellSpacing = 1,
                    CellPadding = 2,
                    Style = GiGraph.Dot.Types.Html.Table.DotHtmlTableStyles.Rounded
                };

                for (var i = 0; i < rows.Length; i++)
                {
                    var cells = rows[i];
                    table.AddRow(row =>
                    {
                        for (var y = 0; y < cells.Length; y++)
                        {
                            var cell = cells[y];
                            var color = store != null && store.Childrens.Colors.ContainsKey(cell.Row, cell.Column) ? store.Childrens.Colors[cell.Row, cell.Column] : default(DotColor);

                            row.AddCell(cell.Context, new DotStyledFont(null, null, color), init =>
                            {
                                init.PortName = cell.PortName;
                                init.ColumnSpan = cell.Column;
                                init.RowSpan = cell.Row;
                            });
                        }
                    });
                }

                if (rows.Length > 0)
                {
                    var node = dot.Nodes.Add(builder.Name);
                    node.Style.CornerStyle = DotCornerStyle.Rounded;
                    node.ToPlainHtmlNode(table);
                }
            }

            foreach (var vertex in Vertices)
                if (vertex.Parent  == null )
                    CreateVertex(vertex);

            foreach (AnalysisEdge edge in Edges)
                CreateEdge(edge);

            foreach (var node in nodes)
                BuildVertex(node.Value);

            dot.SaveToFile(fileName);
        }
    }

    static class DotNodeBuilderHelper 
    {
        public static string FindRoot(Address addr)
        {
            if (addr.Parent != null)
                return FindRoot(addr.Parent);

            return addr.Value;
        }

        public static string FindPort(Address addr)
        {
            if (addr.Parent == null)
                return null;

            return addr.Value;
        }
    }

    class DotNodeBuilder
    {
        public string Name { get; }

        public List<Cell> Cells { get; }

        public List<AnalysisEdge> Edges { get; }

        internal DAGNode Root { get; }

        public DotNodeBuilder(DAGNode root, string name)
        {
            Root = root;
            Name = name;
            Cells = new List<Cell>();
            Edges = new List<AnalysisEdge>();
        }

        public class Cell
        {
            public Cell(int row, int column, string context, string portName)
            {
                Row = row;
                Column = column;
                Context = context;
                PortName = portName;
            }

            public int Row { get; }

            public int Column { get; }

            public string Context { get; }

            public string PortName { get; }
        }
    }

    public abstract class DAGNode : AnalysisNode
    {
        internal DAGNode(Address name, ushort index, bool showName)
            : base(index)
        {
            Name = name;
            ShowName = showName;
        }

        public DAGNode Parent { get; internal set; }

        internal Address Name { get; }

        internal bool ShowName { get; }

        internal abstract int BuildDot(DotNodeBuilder builder, Metadatas metadatas, int row, int column);
    }

    public class DAGLoadNode : DAGNode
    {
        internal DAGLoadNode(Address name, ushort index, bool showName, DAGStoreNode store, DAGNode[] fields) 
            : base(name, index, showName)
        {
            Store = store;
            Fields = fields;
        }

        public DAGNode Store { get; }

        public DAGNode[] Fields { get; }

        internal override int BuildDot(DotNodeBuilder builder, Metadatas metadatas, int row, int column)
        {
            throw new NotImplementedException();
        }
    }

    public class DAGStoreNode : DAGNode
    {
        internal DAGStoreNode(Address name, ushort index, bool showName) 
            : base(name, index, showName)
        {
            Childrens = new DAGNodeCollection(this);
        }

        public DAGNodeCollection Childrens { get; }

        internal override int BuildDot(DotNodeBuilder builder, Metadatas metadatas, int row, int column) 
        {
            if (ShowName)
                builder.Cells.Add(new DotNodeBuilder.Cell(row, column, Name.Value, DotNodeBuilderHelper.FindPort(Name)));

            var maxRow = (Childrens.Count == 0 ? -1 : Childrens.Max(x => x.Name.UseIndex)) + 1;
            for (var i = 0; i < maxRow; i++)
                builder.Cells.Add(new DotNodeBuilder.Cell(row + 1 + i, column, i.ToString(), null));

            // 根据名称分类
            foreach (var group in Childrens.Columns)
                builder.Cells.Add(new DotNodeBuilder.Cell(row, ++column, group.Key, null));

            return row + maxRow + 1;
        }
    }

    public struct DAGNodeCollectionColumn 
    {
        public DAGNodeCollectionColumn(DAGNode node, int column)
        {
            Node = node;
            Column = column;
        }

        public DAGNode Node { get; }

        public int Column { get; }
    }

    public class DAGNodeCollection : IEnumerable<DAGNode>
    {
        private DAGNode mNode;
        private List<DAGNode> mNodes;

        public int Count => mNodes.Count;

        internal TwoKeyDictionary<int, int, Color> Colors { get; }

        internal Dictionary<string, DAGNodeCollectionColumn> Columns { get; }

        internal DAGNodeCollection(DAGNode node) 
        {
            mNode = node;
            mNodes = new List<DAGNode>();
            Colors = new TwoKeyDictionary<int, int, Color>();
            Columns = new Dictionary<string, DAGNodeCollectionColumn>();
        }

        public IEnumerator<DAGNode> GetEnumerator()
        {
            return mNodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal void Add(DAGNode node)
        {
            node.Parent = mNode;
            mNodes.Add(node);

            if (!Columns.ContainsKey(node.Name.Identifier))
                Columns[node.Name.Identifier] = new DAGNodeCollectionColumn(node, Columns.Count);
            else
                Columns[node.Name.Identifier] = new DAGNodeCollectionColumn(node, Columns[node.Name.Identifier].Column);
        }
    }

    public class DAGMicrocodeNode : DAGNode
    {
        internal DAGMicrocodeNode(Address name, ushort index, Microcode atom, bool showName)
            : base(name, index, showName)
        {
            Microcode = atom;
        }

        public Microcode Microcode { get; }


        internal override int BuildDot(DotNodeBuilder builder, Metadatas metadatas, int row, int column)
        {
            if (row == -1) row = builder.Cells.Count;
            if (column == -1) column = 0;

            if (ShowName)
                builder.Cells.Add(new DotNodeBuilder.Cell(row, column, Name.Value, DotNodeBuilderHelper.FindPort(Name)));

            builder.Cells.Add(new DotNodeBuilder.Cell(row, column + 1, Microcode.ToString(metadatas), null));
            return row;
        }
    }
}
