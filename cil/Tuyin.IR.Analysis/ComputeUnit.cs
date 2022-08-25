using Tuyin.IR.Analysis.Data;

namespace Tuyin.IR.Analysis
{
    /// <summary>
    /// 计算单元
    /// </summary>
    public sealed class ComputeUnit : AnalysisNode
    {
        internal ComputeUnit(ushort index, string name, CFG cfg, DAG dag, Vector vet)
            : base(index)
        {
            Name = name;
            CFG = cfg;
            DAG = dag;
            Vector = vet;
        }

        public string Name { get; }
        public CFG CFG { get; }
        public DAG DAG { get; }
        public Vector Vector { get; }
    }
}
