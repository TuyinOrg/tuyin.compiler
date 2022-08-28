using Tuyin.IR.Analysis.Data;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Analysis
{
    /// <summary>
    /// 计算单元
    /// </summary>
    public sealed class ComputeUnit : AnalysisNode
    {
        internal ComputeUnit(ushort index, Function func, Branch bra, CFG cfg, DAG dag, Vector vet)
            : base(index)
        {
            Function = func;
            Branch = bra;
            CFG = cfg;
            DAG = dag;
            Vector = vet;
        }

        public Function Function { get; }
        public Branch Branch { get; }
        public CFG CFG { get; }
        public DAG DAG { get; }
        public Vector Vector { get; }
    }
}
