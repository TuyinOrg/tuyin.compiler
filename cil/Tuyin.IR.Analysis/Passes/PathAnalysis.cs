using System.Collections.Generic;
using Tuyin.IR.Analysis.Data;
using System.Linq;

namespace Tuyin.IR.Analysis.Passes
{
    internal class PATHAnalysis : IAnalysis<PATHAnalysisOpation, PATH>
    {
        public PATH Run(PATHAnalysisOpation input)
        {
            List<AnalysisEdge> edges = new List<AnalysisEdge>();

            // 选择有效终结点
            

            return null;
        }
    }

    class PATHAnalysisOpation
    {
        internal PATHAnalysisOpation(CFG cfg, DAG dag)
        {
            CFG = cfg;
            DAG = dag;
        }

        public CFG CFG { get; }

        public DAG DAG { get; }
    }
}
