namespace Tuyin.IR.Analysis
{
    public interface IAnalysis<TInput, TOutput>
    {
        TOutput Run(TInput input);
    }
}
