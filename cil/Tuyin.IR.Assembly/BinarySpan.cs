namespace Tuyin.IR.Assembly
{
    public struct BinarySpan
    {
        public int StartIndex { get; }

        public int EndIndex { get; }

        public BinarySpan(int start, int end)
        {
            StartIndex = start;
            EndIndex = end;
        }
    }
}

