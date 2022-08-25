using GiGraph.Dot.Entities.Graphs;
using GiGraph.Dot.Extensions;
using GiGraph.Dot.Types.Edges;
using GiGraph.Dot.Types.Records;
using GiGraph.Dot.Types.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using Tuyin.IR.Analysis.Utils;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Analysis.Data
{
    /// <summary>
    /// 控制流
    /// </summary>
    public partial class CFG : AnalysisGraphBase<CFGBlockNode>, IDisposable
    {
        internal CFG(DynamicArray<AnalysisEdge> edges, CFGBlockNode[] vertexs, IReadOnlyList<Statment> statments, Metadatas metadatas)
        {
            Edges = edges;
            Vertices = vertexs;
            Statments = statments;
            Metadatas = metadatas;
        }

        ~CFG() 
        {
            Dispose();
        }

        public Metadatas Metadatas { get; }

        public IReadOnlyList<Statment> Statments { get; }

        public override IReadOnlyList<AnalysisEdge> Edges { get; }

        public override IReadOnlyList<CFGBlockNode> Vertices { get; }

        public void Dispose()
        {
            (Edges as IDisposable)?.Dispose();
        }

        public override void SaveToFile(string fileName)
        {

            void CreateNode(DotGraph dot, CFGBlockNode state) 
            {
                var label = $"label {state.Index}";
                var builder = new DotRecordBuilder()
                    .AppendField(label)
                    .AppendField(string.Join(System.Environment.NewLine, Statments.GetRange(state.Scope.Start, state.Scope.End - state.Scope.Start).Select(x => x.ToString())));

                if (state.Rights.Count > 0)
                {
                    if (state.Rights.Count == 1)
                    {
                        builder = new DotRecordBuilder().AppendRecord(builder.Build());
                        dot.Edges.Add(label, $"label {state.Rights[0].Target.Index}");
                    }
                    else if (state.Rights.Count == 2)
                    {
                        var test = Statments[state.Scope.End - 1] as Test;
                        builder = new DotRecordBuilder().AppendRecord(builder.AppendRecord(rb => rb.AppendField("T", "true").AppendField("F", "false")).Build());
                        dot.Edges.Add(label, $"label {state.Rights[0].Target.Index}", edge => edge.Tail.Endpoint.Port = new DotEndpointPort("false", DotCompassPoint.Center));
                        dot.Edges.Add(label, $"label {state.Rights[1].Target.Index}", edge => edge.Tail.Endpoint.Port = new DotEndpointPort("true", DotCompassPoint.Center));
                    }
                    else throw new NotImplementedException("Switch dot graph's node not implemented.");
                }
                else builder = new DotRecordBuilder().AppendRecord(builder.Build());

                var node = dot.Nodes.Add(label);
                node.Style.CornerStyle = DotCornerStyle.Rounded;
                node.ToRecordNode(builder.Build());
            }

            var dot = new DotGraph(directed: true);
            var graph = this as IAnalysisGraph<CFGBlockNode>;
            foreach (CFGBlockNode node in Vertices)
                    CreateNode(dot, node);

            dot.SaveToFile(fileName);
        }
    }

    [Flags]
    public enum CFGNodeFlags
    {
        None        = 0,
        Case0       = 1 << 2,
        Case1       = 1 << 4,
        CaseN       = 1,
        Test        = 2
    }

    public abstract class CFGNode : AnalysisNode
    {
        public CFGNode(ushort index)
            : base(index)
        {
        }

        public virtual CFGNodeFlags Flags => CFGNodeFlags.None;
    }

    public class CFGBlockNode : CFGNode
    {
        public CFGBlockNode(ushort index, Scope scope)
            : base(index)
        {
            Scope = scope;
        }

        public Scope Scope { get; }
    }
}
