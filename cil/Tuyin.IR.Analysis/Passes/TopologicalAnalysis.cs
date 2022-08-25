using libgraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tuyin.IR.Analysis.Passes
{
    class TopologicalAnalysis : IAnalysis<TopologicalAnalysisOpation, AnalysisNode[]>
    {
        public AnalysisNode[] Run(TopologicalAnalysisOpation input)
        {
            return input.Mode switch
            {
                 TopologicalAnalysisMode.Forward => ComputeForwardTopologicalSort(input.Graph),
                 TopologicalAnalysisMode.Backward => ComputeBackwardTopologicalSort(input.Graph),
                 _ => throw new NotImplementedException()
            };
        }

        public static AnalysisNode[] ComputeForwardTopologicalSort(IAnalysisGraph<AnalysisNode> graph)
        {
            // reverse postorder traversal from entry node
            var length = graph.GetVertices().Count();
            var stack = new Stack<AnalysisNode>();
            var result = new AnalysisNode[length];
            var status = new TopologicalSortNodeStatus[length];
            var index = length - 1;

            foreach (var node in graph.Entries)
            {
                stack.Push(node);
                status[node.Index] = TopologicalSortNodeStatus.FirstVisit;
            }

            do
            {
                var node = stack.Peek();
                var node_status = status[node.Index];

                if (node_status == TopologicalSortNodeStatus.FirstVisit)
                {
                    status[node.Index] = TopologicalSortNodeStatus.SecondVisit;

                    foreach (var succ in node.Rights)
                    {
                        var succ_status = status[succ.Target.Index];

                        if (succ_status == TopologicalSortNodeStatus.NeverVisited)
                        {
                            stack.Push(succ.Target);
                            status[succ.Target.Index] = TopologicalSortNodeStatus.FirstVisit;
                        }
                    }
                }
                else if (node_status == TopologicalSortNodeStatus.SecondVisit)
                {
                    stack.Pop();
                    result[index] = node;
                    index--;
                }
            }
            while (stack.Count > 0);

            return result;
        }

        public static AnalysisNode[] ComputeBackwardTopologicalSort(IAnalysisGraph<AnalysisNode> graph)
        {
            // reverse postorder traversal from exit node
            var length = graph.GetVertices().Count();
            var stack = new Stack<AnalysisNode>();
            var result = new AnalysisNode[length];
            var status = new TopologicalSortNodeStatus[length];
            var index = length - 1;

            foreach (var node in graph.Exits)
            {
                stack.Push(node);
                status[node.Index] = TopologicalSortNodeStatus.FirstVisit;
            }

            do
            {
                var node = stack.Peek();
                var node_status = status[node.Index];

                if (node_status == TopologicalSortNodeStatus.FirstVisit)
                {
                    status[node.Index] = TopologicalSortNodeStatus.SecondVisit;

                    foreach (var pred in node.Lefts)
                    {
                        var pred_status = status[pred.Source.Index];

                        if (pred_status == TopologicalSortNodeStatus.NeverVisited)
                        {
                            stack.Push(pred.Source);
                            status[pred.Source.Index] = TopologicalSortNodeStatus.FirstVisit;
                        }
                    }
                }
                else if (node_status == TopologicalSortNodeStatus.SecondVisit)
                {
                    stack.Pop();
                    result[index] = node;
                    index--;
                }
            }
            while (stack.Count > 0);

            return result;
        }

        enum TopologicalSortNodeStatus
        {
            NeverVisited, // never pushed into stack
            FirstVisit, // pushed into stack for the first time
            SecondVisit // pushed into stack for the second time
        }
    }

    class TopologicalAnalysisOpation
    {
        public TopologicalAnalysisOpation(TopologicalAnalysisMode mode, IAnalysisGraph<AnalysisNode> graph)
        {
            Mode = mode;
            Graph = graph;
        }

        public TopologicalAnalysisMode Mode { get; }

        public IAnalysisGraph<AnalysisNode> Graph { get; }
    }

    enum TopologicalAnalysisMode 
    {
        Forward,
        Backward
    }
}
