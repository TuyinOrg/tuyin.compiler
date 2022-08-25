using AngouriMath.Extensions;
using System.Collections.Generic;
using System.Linq;
using Tuyin.IR.Analysis.Data;

namespace Tuyin.IR.Analysis.Passes
{
    class VectorAnalysis : IAnalysis<VectorAnalysisOpation, Vector>
    {
        public Vector Run(VectorAnalysisOpation input)
        {
            // 从顶层查找非确定终结点来开始构造向量分析
            foreach (var node in input.DAG.Vertices.Where(x => x.Rights.Count > 1))
            {
                var src = node.Lefts.Count == 1 ? node.Lefts[0].Source : node;
                //var nums = node.Rights.Select(x => );

            }

            return new Vector();
        }
    }

    class VectorAnalysisOpation
    {
        public VectorAnalysisOpation(DAG dAG, CFG cFG)
        {
            DAG = dAG;
            CFG = cFG;
        }

        public DAG DAG { get; }
        public CFG CFG { get; }
    }
}
