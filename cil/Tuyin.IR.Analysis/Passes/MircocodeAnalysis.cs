using System;
using System.Collections.Generic;
using System.Text;
using Tuyin.IR.Analysis.Data;

namespace Tuyin.IR.Analysis.Passes
{
    internal class MircocodeAnalysis : IAnalysis<MircocodeAnalysisOpations, IEnumerable<Microcode>>
    {
        public IEnumerable<Microcode> Run(MircocodeAnalysisOpations input)
        {
            DynamicArray<Microcode> codes = new DynamicArray<Microcode>(input.DAG.Vertices.Count);
            Stack<AnalysisNode> nodes = new Stack<AnalysisNode>();
            nodes.Push(input.DAG.Entry);

            while (nodes.Count > 0)
            {
                var node = nodes.Pop();
                if (node is DAGMicrocodeNode code)
                    codes.Add(code.Microcode);

                foreach (var right in node.Rights)
                    nodes.Push(right.Target);
            }

            return codes;
        }
    }

    internal class MircocodeAnalysisOpations 
    {
        public MircocodeAnalysisOpations(DAG dAG)
        {
            DAG = dAG;
        }

        public DAG DAG { get; }
    }
}
