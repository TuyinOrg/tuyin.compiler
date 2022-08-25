namespace Tuyin.IR.Analysis.Utils.Colors;

struct Range
{
    /// <summary>
    /// Lower bound of the range.
    /// </summary>
    public int Lower { get; set; }
    
    /// <summary>
    /// Upper bound of the range.
    /// </summary>
    public int Upper { get; set; }
    
    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="lower"></param>
    /// <param name="upper"></param>
    public Range(int lower, int upper)
    {
        Lower = lower;
        Upper = upper;
    }
}