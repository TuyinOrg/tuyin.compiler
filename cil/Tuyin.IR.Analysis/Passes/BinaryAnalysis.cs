using System;
using System.Collections.Generic;
using Tuyin.IR.Reflection;
using System.Linq;

namespace Tuyin.IR.Analysis.Passes
{
    internal class BinaryAnalysis : IAnalysis<BinaryAnalysisOpation, IEnumerable<Statment>>
    {
        public IEnumerable<Statment> Run(BinaryAnalysisOpation input)
        {
            var queue = new Stack<AnalysisNode>(input.Edges.Where(x => x.Source.Lefts.Count == 0).Select(x => x.Source));


            throw new NotImplementedException();
        }
    }

    class BinaryAnalysisOpation 
    {
        public BinaryAnalysisOpation(Metadatas metadatas, IReadOnlyList<AnalysisEdge> edges)
        {
            Metadatas = metadatas;
            Edges = edges;
        }

        public Metadatas Metadatas { get; }

        public IReadOnlyList<AnalysisEdge> Edges { get; }
    }
}
