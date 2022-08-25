public struct SourceSpan : ISourceSpan
{
    public int StartIndex { get; }

    public int EndIndex { get; }

    public SourceSpan(int start, int end)
    {
        StartIndex = start;
        EndIndex = end;
    }

    public SourceSpan(ISourceSpan sourceSpan)
    {
        StartIndex = sourceSpan.StartIndex;
        EndIndex = sourceSpan.EndIndex;
    }

    public override string ToString()
    {
        return $"{StartIndex}-{EndIndex}";
    }
}