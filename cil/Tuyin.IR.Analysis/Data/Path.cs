namespace Tuyin.IR.Analysis.Data
{
    public class PATH
    { 
        public AnalysisEdge[] Vailds { get; }

        public AnalysisEdge[] Invailds { get; }

        public PATH(AnalysisEdge[] vailds, AnalysisEdge[] invailds)
        {
            Vailds = vailds;
            Invailds = invailds;
        }
    }
}
