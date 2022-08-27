using System.Collections.Generic;
using Tuyin.IR.Analysis.Data;
using System.Linq;

namespace Tuyin.IR.Analysis.Passes
{
    internal class PATHAnalysis : IAnalysis<PathAnalysisOpation, PATH>
    {
        public PATH Run(PathAnalysisOpation input)
        {
            List<AnalysisEdge> edges = new List<AnalysisEdge>();

            // 选择有效终结点
            var entries = input.DAG.Vertices.Where( x => x.Lefts.Count == 0 && x.Parent == null);

            // 
            var levels = entries.Select(x => new { Level = input.Branch.StatmentBranches[x.StatmentIndex], DAG = x }).ToArray();
            

            Stack<AnalysisNode> nodes = new Stack<AnalysisNode>();
            nodes.Push(input.DAG.Entry);
            while (nodes.Count > 0)
            {
                var node = nodes.Pop();
                if (node is DAGMicrocodeNode code)
                {
                    
                }

                foreach (var right in node.Rights)
                    nodes.Push(right.Target);
            }

            return null;
        }
    }

    class PathAnalysisOpation
    {
        internal PathAnalysisOpation(Branch bra, DAG dag)
        {
            Branch = bra;
            DAG = dag;
        }

        public Branch Branch { get; }

        public DAG DAG { get; }
    }
}
